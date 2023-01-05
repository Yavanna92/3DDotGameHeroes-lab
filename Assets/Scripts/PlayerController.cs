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

    private GameObject _hugeSword;

    private float _swordTime;

    private bool _hasBoomerang;

    [SerializeField]
    private GameObject _boomerangPrefab;

    private void Awake()
    {
        _sword = GameObject.FindGameObjectWithTag("Sword");
    }
    [SerializeField]
    private float _boomerangSpeed;

    [SerializeField]
    private Transform _boomerangSpawnPoint;

    private Rigidbody _boomerangRb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        PlayerPos = _rb.position;
        _anim = GetComponent<Animator>();

        _sword.SetActive(false);

        _hugeSword = GameObject.FindGameObjectWithTag("HugeSword");

        _hugeSword.SetActive(false);

        _swordTime = 0;

        _hasBoomerang = true;

        _boomerangPrefab = Instantiate(_boomerangPrefab);

        _boomerangRb = _boomerangPrefab.GetComponent<Rigidbody>();

        _boomerangPrefab.GetComponent<BoomerangController>().Speed = _boomerangSpeed;
        _boomerangPrefab.GetComponent<BoomerangController>().ParentTransform = _boomerangSpawnPoint;
    }

    // Update is called once per frame
    private void Update()
    {
        var dir = new Vector3(0f, 0f, 0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameSystem.Instance.PlayerHealth == 10)
            {
                // reset the sword transform
                _hugeSword.transform.localPosition = new Vector3(0.0f, 0.5f, 0.5f);
                _hugeSword.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                _hugeSword.SetActive(true);
            }
            else
            {
                // reset the sword transform
                _sword.transform.localPosition = new Vector3(0.0f, 0.5f, 0.5f);
                _sword.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                _sword.SetActive(true);
            }

            if (_sword.activeSelf && _hugeSword.activeSelf)
            {
                if (GameSystem.Instance.PlayerHealth == 10)
                    _sword.SetActive(false);
                else
                    _hugeSword.SetActive(false);
            }

            // hardcoded 60f correspond to the attack animation duration
            _swordTime = Time.fixedDeltaTime * 60f;
            _anim.Play("Attack");
        }
        else
        {
            if (_swordTime <= 0f)
            {
                if (GameSystem.Instance.PlayerHealth == 10)
                    _hugeSword.SetActive(false);
                else
                    _sword.SetActive(false);

                if (Input.GetKeyDown(KeyCode.E) && GameSystem.Instance.HasBoomerang && _hasBoomerang)
                    ThrowBoomerang();

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



    public void ResetPos()
    {
        _anim.gameObject.SetActive(false);
        gameObject.transform.position = new Vector3(0f, 0f, -27f);
        gameObject.transform.rotation = Quaternion.identity;
        _anim.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Destroy(_sword);
    }

    private void ThrowBoomerang()
    {
        _boomerangPrefab.GetComponentInChildren<BoomerangController>().SetActive();
        _boomerangPrefab.transform.position = _boomerangSpawnPoint.position;
        var rot = _boomerangPrefab.transform.rotation.eulerAngles;
        _boomerangPrefab.transform.rotation = Quaternion.Euler(rot.x - 90f, rot.y, rot.z);

        _boomerangRb.MovePosition(_boomerangSpawnPoint.position);
        _boomerangRb.MoveRotation(_boomerangSpawnPoint.rotation);
        _boomerangRb.velocity = _boomerangSpawnPoint.forward * _boomerangSpeed;

        _hasBoomerang = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boomerang"))
            _hasBoomerang = true;
    }

    public bool GetHasBoomerang()
    {
        return _hasBoomerang;
    }

    public void SetHasBoomerang(bool hasBoomerang)
    {
        _hasBoomerang = hasBoomerang;
    }
}
