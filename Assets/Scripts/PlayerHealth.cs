using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health { get; set; }
    private const int _MAX_HEALTH = 10;

    // Start is called before the first frame update
    void Start()
    {
        Health = _MAX_HEALTH;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            //Destroy(gameObject);
            Health = 0;
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}