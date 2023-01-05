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

    private Vector3 _previousPos;

    //private float _directionTimer = 0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        _previousPos = transform.position;
    }
    private void Update()
    {
        var dir = Vector3.zero;
        var rot = transform.rotation.eulerAngles;

        // skeleton only sees in orthogonal directions depending on its angle
        if (rot.y > -45f && rot.y <= 45f || rot.y > 315f)
            dir = Vector3.forward;

        else if (rot.y > 45f && rot.y <= 135f)
            dir = Vector3.right;
        
        else if (rot.y > 135f && rot.y <= 225f || rot.y <= -135)
            dir = Vector3.back;
        
        else if (rot.y > 225 && rot.y <= 315 || rot.y < -90 && rot.y >= -135)
            dir = Vector3.left;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckCapsule(transform.position, transform.position + dir * sightRange, 0.8f, playerMask);
        playerInAttackRange = Physics.CheckCapsule(transform.position, transform.position + dir * attackRange, 0.8f, playerMask);

        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            Chase();
        if (playerInSightRange && playerInAttackRange)
            Attack();

        _previousPos = transform.position;
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
        else
        {
            agent.SetDestination(walkPoint);
            transform.LookAt(walkPoint);
            _anim.Play("Walk");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        Vector3 distanceWalked = transform.position - _previousPos;

        if ((distanceToWalkPoint.magnitude < 1f || distanceWalked.magnitude != 0f) && (distanceToWalkPoint.magnitude >= 1f || distanceWalked.magnitude == 0f))
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

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        _anim.Play("Attack");

        if (alreadyAttacked)
        {
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        _anim.Play("Iddle");

        alreadyAttacked = false;
    }

    public void ChangePos(Vector2 newPos)
    {
        gameObject.transform.position = new Vector3(newPos.x, 0.0f, newPos.y);
    }

}

