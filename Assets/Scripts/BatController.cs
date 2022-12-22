using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BatController : MonoBehaviour
{
    private float _timer;
    private float _cooldown;
    private bool _isAttacking;

    [SerializeField]
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0.0f; _cooldown = 10.0f; _isAttacking = false;
        _rb = GetComponent<Rigidbody>();
        _rb.position = new Vector3(0.0f, 1.0f, -21.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 dir;

        // attack
        _timer += Time.fixedDeltaTime;


        if (_isAttacking)
        {
            dir = new Vector3(0.0f, (float)Math.Sin(Time.time * 5.0f) / 1.5f, 0.0f);
            if ((float)Math.Sin(Time.time * 5.0f) == 0)
            {
                _isAttacking = false;
                _timer = 0.0f;
            }
        }
        else if (!_isAttacking && _timer >= _cooldown)
        {
            dir = new Vector3(0.0f, (float)Math.Sin(Time.time * 5.0f) / 1.5f, 0.0f);
            _isAttacking = true;
        }

        // vertical oscillation
        dir = new Vector3(0.0f, (float)Math.Sin(Time.time*5.0f)/1.5f, 0.0f);

        _rb.velocity = dir;
    }
}
