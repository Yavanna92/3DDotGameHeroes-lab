using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public EnemyHealthController health;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health = collision.gameObject.GetComponent<EnemyHealthController>();

            if (health != null)
                health.RemoveHealth();
        }
    }
}
