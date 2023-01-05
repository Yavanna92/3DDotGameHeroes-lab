using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObjectController : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.FindGameObjectWithTag("Player");
        //gameObject.transform.position = _player.transform.position;
        Invoke(nameof(SelfDestroy), 120f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            GameSystem.Instance.IncrementHealth();
            Destroy(gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
