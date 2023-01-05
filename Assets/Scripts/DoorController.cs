using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!open && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("StayOpen"))
        {
            gameObject.SetActive(false);
            open = true;
        }
        else if (open && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            open = false;
        }
    }
}
