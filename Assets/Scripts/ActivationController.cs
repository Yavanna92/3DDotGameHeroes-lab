using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    private bool Active;

    // Start is called before the first frame update
    void Start()
    {
        Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Active = true;
        gameObject.GetComponent<Animator>().Play("Activate");
    }

    public bool isActive()
    {
        return Active;
    }
}
