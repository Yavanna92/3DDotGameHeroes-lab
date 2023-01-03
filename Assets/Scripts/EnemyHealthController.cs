using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health = 2;

    public void RemoveHealth()
    {
        health--;

        if (health <= 0)
            Destroy(gameObject);
    }
}