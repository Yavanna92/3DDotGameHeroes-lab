using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_playerHealth == null)
                _playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            _playerHealth.TakeDamage(2);

            gameObject.GetComponentInParent<SkeletonController>().SetAlreadyAttacked();
        }
    }
}