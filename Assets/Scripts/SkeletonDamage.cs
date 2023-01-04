using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(2);

            gameObject.GetComponentInParent<SkeletonController>().SetAlreadyAttacked();
        }
    }
}