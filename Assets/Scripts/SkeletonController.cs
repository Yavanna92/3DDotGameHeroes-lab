using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform _playerTransform;

    public LayerMask groundMask, playerMask;

    [SerializeField]
    private Animator _anim;

    // patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool _movingTowardsX = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        var dir = Vector3.zero;
        var rot = transform.rotation;

        if (rot.y < 45 && rot.y >= 135)
        {
            dir = Vector3.right;
        }
        else if (rot.y < 135 && rot.y >= 225)
        {
            dir = Vector3.back;
        }
        else if (rot.y < 225 && rot.y >= 315)
        {
            dir = Vector3.left;
        }
        else
        {
            dir = Vector3.forward;
        }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckCapsule(transform.position, transform.position + dir * sightRange, 0.8f, playerMask);
        playerInAttackRange = Physics.CheckCapsule(transform.position, transform.position + dir * attackRange, 0.8f, playerMask);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            Chase();
        if (playerInSightRange && playerInAttackRange)
            Attack();
    }

    public void SetAlreadyAttacked()
    {
        alreadyAttacked = true;
    }

    private void Patroling()
    {
        if (agent.speed > 3.5f)
            agent.speed = 3.5f;

        if (!walkPointSet)
            SearchWalkPoint();
        else { 
            agent.SetDestination(walkPoint);
            transform.LookAt(walkPoint);
            _anim.Play("Walk");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
            _movingTowardsX = !_movingTowardsX;
        }
    }

    private void SearchWalkPoint()
    {
        float rand = Random.Range(-walkPointRange, walkPointRange);

        if (_movingTowardsX)
            walkPoint = new Vector3(transform.position.x + rand, transform.position.y, transform.position.z);
        else
            walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + rand);

        if (Physics.Raycast(walkPoint, -transform.up, 2.0f, groundMask))            
            walkPointSet = true;
        
        else
            _movingTowardsX = !_movingTowardsX;
    }

    private void Chase()
    {
        if (agent.speed < 8)
            agent.speed = 8;

        _anim.Play("Walk");

        agent.SetDestination(_playerTransform.position);
        transform.LookAt(_playerTransform.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(_playerTransform.position);

        _anim.Play("Attack");

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _anim.Play("Iddle");
        alreadyAttacked = false;
    }
}
