using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonShot : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_playerHealth == null)
                _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            _playerHealth.TakeDamage(1);

        }
        gameObject.SetActive(false);
    }

    public void Shoot()
    {       
        gameObject.SetActive(true);
    }
}
