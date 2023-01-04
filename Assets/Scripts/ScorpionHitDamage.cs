using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionHitDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerHealth != null)
                playerHealth.TakeDamage(1);
        }

        gameObject.GetComponentInParent<ScorpionController>().SetAlreadyAttacked();
    }
}
