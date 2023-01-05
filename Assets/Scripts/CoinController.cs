using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Vector3 Pos { get; private set; }
    private GameObject _player;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            GameSystem.Instance.IncrementGold();
            Destroy(gameObject);
        }
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
