using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    [SerializeField]
    private GameObject _boss;

    // Start is called before the first frame update
    void Start()
    {
        _boss = Instantiate(_boss);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        Destroy(_boss);
    }

}
