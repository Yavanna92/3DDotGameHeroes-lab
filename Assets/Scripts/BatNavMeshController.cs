using UnityEngine;
using System;
using Assets.Sources;
using UnityEngine.AI;

public class BatNavMeshController : MonoBehaviour
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

    private Transform _playerTransform;

    #endregion

    private void Awake()
    { 
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
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

        transform.LookAt(_playerTransform);

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
}
