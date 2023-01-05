using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponentInChildren<GameObject>().SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

}
