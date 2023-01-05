using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    public LayerMask _groundMask;

    [SerializeField]
    public LayerMask _playerMask;

    [SerializeField]
    public LayerMask _waterMask;

    // patroling
    [SerializeField]
    private Vector3 _walkPoint;

    [SerializeField]
    private bool _walkPointSet;

    [SerializeField]
    private float _walkPointRange;

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
    private Transform _bulletSpawnPoint;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private float _cooldown;

    private Rigidbody _bulletRb;

    private Transform _playerTransform;

    private float _timer;

    #endregion

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _bulletPrefab = Instantiate(_bulletPrefab);
    }

    private void Start()
    {
        _timer = 0.0f;
        _bulletRb = _bulletPrefab.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        // boss always looks at player
        transform.LookAt(_playerTransform);

        _timer += Time.fixedDeltaTime;

        if (_timer >= _cooldown * Time.fixedDeltaTime)
        {
            _bulletPrefab.GetComponent<FireShot>().Shoot();
            _bulletRb.MovePosition(_bulletSpawnPoint.position);
            _bulletRb.MoveRotation(_bulletSpawnPoint.rotation);
            _bulletRb.MoveRotation(Quaternion.Euler(-90f, 0f, 0f));
            _bulletRb.velocity = _bulletSpawnPoint.forward * _bulletSpeed;

            _timer = 0.0f;
            gameObject.GetComponent<Animator>().enabled = true;
        }

        if (_timer <= (_cooldown * Time.fixedDeltaTime) / 2)
            gameObject.GetComponent<Animator>().enabled = false;

        //Check for sight and attack range
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerMask);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerMask);

        if (!_playerInSightRange && !_playerInAttackRange)
            Patroling();
        else if (_playerInSightRange)
            Chase();
    }

    private void Patroling()
    {
        if (!_walkPointSet)
            SearchWalkPoint();
        if (_walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            _walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randX = Random.Range(-_walkPointRange, _walkPointRange);
        float randZ = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2.0f, _groundMask) && !Physics.Raycast(_walkPoint, -transform.up, 2.0f, _waterMask))
            _walkPointSet = true;
    }

    private void Chase()
    {
        _agent.SetDestination(_playerTransform.position);

        if (_playerInAttackRange)
            transform.LookAt(_playerTransform);
    }
}
