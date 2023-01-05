using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int health = 2;

    [SerializeField]
    private GameObject _healthObject;

    [SerializeField]
    private GameObject _coinObject;

    public void RemoveHealth()
    {
        health--;

        if (health <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                var valueObject = Random.Range(0, 4);
                if (valueObject == 2)
                {
                    var trans = gameObject.GetComponent<Transform>();

                    Instantiate(_healthObject, new Vector3(trans.position.x, 0.5f, trans.position.z), Quaternion.Euler(-90f, 0f, 0f));
                }
                else if (valueObject == 1)
                {
                    var trans = gameObject.GetComponent<Transform>();

                    Instantiate(_coinObject, new Vector3(trans.position.x, 0.5f, trans.position.z), Quaternion.Euler(-90f, 0f, 0f));
                }
                Destroy(gameObject);
            }
        }
    }
}