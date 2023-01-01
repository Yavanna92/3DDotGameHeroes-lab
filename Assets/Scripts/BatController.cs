using UnityEngine;
using System;
using Assets.Sources;
using UnityEngine.AI;

public class BatController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    public LayerMask _groundMask;

    [SerializeField]
    public LayerMask _playerMask;

    // patroling
    [SerializeField]
    private Vector3 _walkPoint;

    [SerializeField]
    private bool _walkPointSet;

    [SerializeField]
    private float _walkPointRange;

    // attacking
    [SerializeField]
    private float _timeBetweenAttacks;

    [SerializeField]
    private bool _alreadyAttacked;

    // states
    [SerializeField]
    private float _sightRange;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private bool _playerInSightRange;

    [SerializeField]
    private bool _playerInAttackRange;

    [SerializeField]
    private Rigidbody _rb;

    private Transform _playerTransform;
    private float _timer;
    private float _cooldown;
    private bool _isAttacking;
    private BatAttackState _batAttackState;

    public Vector2 InitialPos { get; set; }

    #endregion

    private void Awake()
    { 
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // init state
        _timer = 0.0f; _cooldown = 10.0f; _isAttacking = false;
        _batAttackState = BatAttackState.StartingPoint;

        // init physics -> get rigid body
        _rb = GetComponent<Rigidbody>();

        // place enemy
        _rb.position = new Vector3(0.0f, 1.6f, -21.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 dir = new Vector3(0.0f, 0.0f, 0.0f);

        _timer += Time.fixedDeltaTime;

        if (_isAttacking)
        {
            dir = UpdateOnAttack();
        }
        else if (!_isAttacking && _timer >= _cooldown)
        {
            _rb.MovePosition(new Vector3(_rb.position.x, 1.6f, _rb.position.z));
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            _isAttacking = true;
            _timer = 0.0f;
        }
        else {
            // vertical oscillation
            dir = new Vector3(0.0f, (float)Math.Sin(Time.time*3f)*0.3f, 0.0f);
        }

        _rb.velocity = dir;

        //Check for sight and attack range
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerMask);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerMask);

        if (!_playerInSightRange && !_playerInAttackRange)
            Patroling();
        else if (_playerInSightRange && !_playerInAttackRange)
            Chase();
        else if (_playerInSightRange && _playerInAttackRange)
            Attack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _batAttackState = BatAttackState.Rising;

        _rb.velocity = new Vector3(0.0f, 2f, 0.0f);
    }

    private void Patroling()
    {
        if (!_walkPointSet)
            SearchWalkPoint();
        else
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2.0f, _groundMask))
            _walkPointSet = true;
    }

    private void Chase()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);

        _rb.rotation = Quaternion.LookRotation(_playerTransform.position, Vector3.up);

        UpdateOnAttack();

        if (!_alreadyAttacked)
        {
            _alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

        // TODO: Make sure it is not necessary to adjust the time in which bat stays at the minimum height
    // to be able to attack it
    private Vector3 UpdateOnAttack()
    {
        Vector3 dir = new Vector3(0.0f, 0.0f, 0.0f);

        switch (_batAttackState)
        {
            case BatAttackState.StartingPoint:

                _batAttackState = BatAttackState.Falling;

                break;

            case BatAttackState.Falling:

                if (_rb.position.y <= 0.6f)
                    _batAttackState = BatAttackState.TargetPoint;
                else
                    dir = new Vector3(0.0f, -2f, 0.0f);

                break;

            case BatAttackState.TargetPoint:
                // TODO: test that this enemy can be killed as it is at this moment; if not, add a timer
                // and keep it in this position for enough game cycles to make player able to kill it
                _rb.MovePosition(new Vector3(_rb.position.x, 0.6f, _rb.position.z));

                _batAttackState = BatAttackState.Rising;

                break;

            case BatAttackState.Rising:

                if (_rb.position.y >= 1.5f)
                    _batAttackState = BatAttackState.EndingPoint;
                else
                    dir = new Vector3(0.0f, 2f, 0.0f);

                break;

            case BatAttackState.EndingPoint:
                // reset values
                _rb.MovePosition(new Vector3(_rb.position.x, 1.6f, _rb.position.z));
                _isAttacking = false;
                _timer = 0.0f;
                _batAttackState = BatAttackState.StartingPoint;

                break;
        }

        return dir;
    }
}
