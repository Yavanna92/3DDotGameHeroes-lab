using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonShot : MonoBehaviour
{
    public float life = 3.0f;

    // Update is called once per frame
    void Awake()
    {
        Destroy(gameObject, life);
    }

    void onCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
