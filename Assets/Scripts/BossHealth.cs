using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private KillBossBall[] _killBossBalls;
    private int _index = 5;

    // Start is called before the first frame update
    void Start()
    {
        _killBossBalls = gameObject.GetComponentsInChildren<KillBossBall>();
    }

    public void TakeDamage()
    {
        _killBossBalls[_index].KillBall();

        if (_index == 0)
        {
            Destroy(gameObject);
        }
        _index--;
    }
}
