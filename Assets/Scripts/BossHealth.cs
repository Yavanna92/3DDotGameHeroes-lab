using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private KillBossBall[] _killBossBalls;

    [SerializeField]
    private GameObject _healthObject;

    [SerializeField]
    private GameObject _coinObject;

    private int _index = 5;

    // Start is called before the first frame update
    void Start()
    {
        _killBossBalls = gameObject.GetComponentsInChildren<KillBossBall>();
    }

    public void TakeDamage()
    {
        
        var trans = _killBossBalls[_index].gameObject.GetComponent<Transform>();

        if (_index == 3)
            Instantiate(_healthObject, new Vector3(trans.position.x, 0.5f, trans.position.z), Quaternion.Euler(-90f, 0f, 0f));
        else
            Instantiate(_coinObject, new Vector3(trans.position.x, 0.5f, trans.position.z), Quaternion.Euler(-90f, 0f, 0f));

        _killBossBalls[_index].KillBall();

        if (_index == 0)
        {
            Destroy(gameObject);
        }
        _index--;
    }
}
