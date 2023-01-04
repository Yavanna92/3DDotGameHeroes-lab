using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinController : MonoBehaviour
{
    public Vector3 Pos { get; private set; }

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = Pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
