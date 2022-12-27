using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Windows;
using System.Threading;
using Assets.Scripts.Sources;
using System.Runtime.CompilerServices;

public class BatController : MonoBehaviour
{
    private float _timer;
    private float _cooldown;
    private bool _isAttacking;
    private BatAttackState _batAttackState;

    [SerializeField]
    private Rigidbody _rb;

    // TODO: see how to set from GameSystem
    // coords on plane XZ
    public Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        // init state
        _timer = 0.0f; _cooldown = 10.0f; _isAttacking = false;
        _batAttackState = BatAttackState.StartingPoint;

        // init physics -> instiantate rigid body
        _rb = GetComponent<Rigidbody>();

        // place enemy
        _rb.position = new Vector3(0.0f, 1.5f, -21.0f);
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
            _rb.MovePosition(new Vector3(_rb.position.x, 1.5f, _rb.position.z));
            dir = new Vector3(0.0f, 0.0f, 0.0f);
            _isAttacking = true;
            _timer = 0.0f;
        }
        else {
            // vertical oscillation
            dir = new Vector3(0.0f, (float)Math.Sin(Time.time*3f)*0.3f, 0.0f);
        }

        _rb.velocity = dir;
    }

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
                _rb.MovePosition(new Vector3(_rb.position.x, 1.5f, _rb.position.z));
                _isAttacking = false;
                _timer = 0.0f;
                _batAttackState = BatAttackState.StartingPoint;

                break;
        }

        return dir;
    }
}