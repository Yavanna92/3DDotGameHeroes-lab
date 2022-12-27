using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO: not attached to any prefab. Use this controller as a base to implement scorpion enemy
public class OldBatController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 2;
    public float cooldown = 10.0f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _timer = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // shoot
        _timer += Time.fixedDeltaTime;
        if (_timer >= cooldown) {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            _timer = 0.0f;
        }
        
        //int rand = UnityEngine.Random.Range(0, 500);
        //if (rand == 1) {
        //    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        //    bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        //}

        // vertical oscillation
        var dir = new Vector3(0.0f, (float)Math.Sin(Time.time*5.0f)/1.5f, 0.0f);

        _rb.velocity = dir;
    }
}
