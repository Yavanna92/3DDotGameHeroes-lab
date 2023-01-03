using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthController : MonoBehaviour
{
    public int health = 4;

    public void RemoveHealth()
    {
        health--;

        if (health <= 0)
            Destroy(gameObject);
    }
}
