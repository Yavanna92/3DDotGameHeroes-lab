using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public EnemyHealthController health;

    private BossHealth _bossHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health = collision.gameObject.GetComponent<EnemyHealthController>();

            if (health != null)
                health.RemoveHealth();
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            if (_bossHealth == null)
                _bossHealth = collision.gameObject.GetComponentInParent<BossHealth>();

            _bossHealth.TakeDamage();
        }
        if (collision.gameObject.CompareTag("Activation"))
        {
            collision.gameObject.GetComponent<ActivationController>().Activate();
        }
    }
}
