using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    
    [SerializeField]
    private Rigidbody _rb;

    public Vector3 PlayerPos { get; private set; }

    [SerializeField]
    private Animator _anim;

    private GameObject _sword;

    private float _swordTime;

    public bool _hasBoomerang;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        PlayerPos = _rb.position;
        _anim = GetComponent<Animator>();

        _sword = GameObject.FindGameObjectWithTag("Sword");

        _sword.SetActive(false);

        _swordTime = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        var dir = new Vector3(0f, 0f, 0f);
        if (Input.GetKey(KeyCode.Space))
        {
            // reset the sword transform
            _sword.transform.localPosition = new Vector3(0.0f, 0.5f, 0.5f);
            _sword.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            _sword.SetActive(true);

            // hardcoded 60f correspond to the attack animation duration
            _swordTime = Time.fixedDeltaTime * 60f;
            _anim.Play("Attack");
        }
        else
        {
            if (_swordTime <= 0f)
            {
                _sword.SetActive(false);

                dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                if (dir.x != 0 || dir.z != 0)
                {
                    Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
                    _rb.rotation = rot;

                    _anim.Play("Walk");
                }
            }
            else
            {
                _swordTime -= Time.fixedDeltaTime;

            }
        }
          
        _rb.velocity = dir * _speed * Time.fixedDeltaTime;
        PlayerPos = _rb.position;
    }

    private void OnDestroy()
    {
        Destroy(_sword);
    }

}
