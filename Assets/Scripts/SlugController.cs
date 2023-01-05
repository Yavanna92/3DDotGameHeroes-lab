using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlugController : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform _playerTransform;

    public LayerMask groundMask, playerMask;

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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            Chase();
        if (playerInSightRange && playerInAttackRange)
            Attack();
    }

    private void Patroling()
    {
        if (!walkPointSet)
            SearchWalkPoint();
        else
        {
            agent.SetDestination(walkPoint);
            transform.LookAt(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randX = Random.Range(-walkPointRange, walkPointRange);
        float randZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2.0f, groundMask))
            walkPointSet = true;
    }

    private void Chase()
    {
        agent.SetDestination(_playerTransform.position);
        transform.LookAt(_playerTransform.position);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(_playerTransform.position);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void ChangePos(Vector2 newPos)
    {
        gameObject.transform.position = new Vector3(newPos.x, 0.0f, newPos.y);
    }
}
