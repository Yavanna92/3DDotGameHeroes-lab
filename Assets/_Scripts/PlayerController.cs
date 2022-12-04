using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private Rigidbody _rb;

    private Vector3 _dir;
    //private float _torque;
    // Start is called before the first frame update
    void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        //_rb.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    private void Update()
    {
        //_rb.AddForce();
        _dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //dir.Normalize();
        //_rb.velocity = dir * _speed;
        //_rb.AddTorque(dir * _torque);
        //_rb.rotation.SetLookRotation(dir, Vector3.up);

    }

    private void FixedUpdate()
    {
        _rb.velocity = _dir * _speed;
    }
}
