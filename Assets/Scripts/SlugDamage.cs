using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlugDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(2);
        }
    }
}