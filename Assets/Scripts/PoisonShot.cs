using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonShot : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(1);
        }
        gameObject.SetActive(false);
    }

    public void Shoot()
    {       
        gameObject.SetActive(true);
    }
}
