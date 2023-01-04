using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(playerHealth == null)
                playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(4);
        }
    }
}
