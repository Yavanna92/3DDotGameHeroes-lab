using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    public EnemyHealthController health;

    private BossHealth _bossHealth;

    private bool _hasHit;

    public Transform ParentTransform { get; set; }

    public float Speed { get; set; }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _hasHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasHit)
        {
            var dir = ParentTransform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity += dir.normalized * Speed/5;
        }
    }

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

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sword") || collision.gameObject.CompareTag("HugeSword"))
        {
            gameObject.SetActive(false);
            _hasHit = false;
        }
        else
        {
            _hasHit = true;
        }
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
