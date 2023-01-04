using UnityEngine;
using System;
using Assets.Sources;
using Unity.VisualScripting;

public class BatController : MonoBehaviour
{
    private float _timer;
    private float _cooldown;
    private bool _isAttacking;
    private BatAttackState _batAttackState;
    private float _batTime;
    private float _vulnerabilityTimer;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private GameObject _batNavMesh;


    private void Awake()
    {
        // init AI -> instantiate NavMesh
        _batNavMesh = Instantiate(_batNavMesh);
    }

    // Start is called before the first frame update
    void Start()
    {
        // init state
        _timer = 0.0f; _cooldown = 10.0f; _isAttacking = false;
        _vulnerabilityTimer = 0f;
        _batAttackState = BatAttackState.StartingPoint;

        // init physics -> get rigid body
        _rb = GetComponent<Rigidbody>();

        // place enemy
        _rb.position = new Vector3(_rb.position.x, 1.5f, _rb.position.z);
        //_batNavMesh.GetComponent<Transform>().position = new Vector3(_rb.position.x, 0.07f, _rb.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 dir = new Vector3(0.0f, 0.0f, 0.0f);

        _timer += Time.fixedDeltaTime;
        _batTime += Time.fixedDeltaTime;

        if (_isAttacking)
        {
            dir = UpdateOnAttack();
        }
        else if (!_isAttacking && _timer >= _cooldown)
        {
            _rb.MovePosition(new Vector3(_rb.position.x, 1.5f, _rb.position.z));
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            _isAttacking = true;
            _timer = 0.0f;
        }
        else
        {
            // vertical oscillation
            dir = new Vector3(0.0f, (float)Math.Sin(_batTime * 3f) * 0.15f, 0.0f);
        }

        _rb.velocity = dir;
        var navMeshPos = _batNavMesh.GetComponent<Transform>().position;
        _rb.MovePosition(new Vector3(navMeshPos.x, _rb.position.y, navMeshPos.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        _batAttackState = BatAttackState.Rising;

        _rb.velocity = new Vector3(0.0f, 1.5f, 0.0f);
    }

    private void OnDestroy()
    {
        Destroy(_batNavMesh);
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

                if (_rb.position.y <= 0.5f)
                    _batAttackState = BatAttackState.TargetPoint;
                else
                    dir = new Vector3(0.0f, -1.5f, 0.0f);

                break;

            case BatAttackState.TargetPoint:
                // TODO: test that this enemy can be killed as it is at this moment; if not, add a timer
                // and keep it in this position for enough game cycles to make player able to kill it

                if (_vulnerabilityTimer <= 0.001f)
                {
                    _rb.MovePosition(new Vector3(_rb.position.x, 0.4f, _rb.position.z));
                }

                _vulnerabilityTimer += Time.fixedDeltaTime;

                if (_vulnerabilityTimer >= 40f * Time.fixedDeltaTime)
                    _batAttackState = BatAttackState.Rising;

                break;

            case BatAttackState.Rising:
                if (_vulnerabilityTimer > 0.001f)
                    _vulnerabilityTimer = 0f;
                
                if (_rb.position.y >= 1.4f)
                    _batAttackState = BatAttackState.EndingPoint;
                else
                    dir = new Vector3(0.0f, 1.5f, 0.0f);

                break;

            case BatAttackState.EndingPoint:
                // reset values
                _rb.MovePosition(new Vector3(_rb.position.x, 1.5f, _rb.position.z));
                _isAttacking = false;
                _timer = 0.0f;
                _batAttackState = BatAttackState.StartingPoint;

                break;
        }

        return dir;
    }
}