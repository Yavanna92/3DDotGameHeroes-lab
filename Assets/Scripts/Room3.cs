using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room3 : MonoBehaviour
{

    [SerializeField]
    private GameObject slugObject;

    private GameObject _slug1;
    private GameObject _slug2;
    private GameObject _slug3;
    private GameObject _slug4;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {

        roomCenter = gameObject.GetComponent<Transform>().position;

        _slug1 = Instantiate(slugObject);
        _slug1.GetComponent<SlugController>().ChangePos(new Vector2(roomCenter.x - 6.0f, roomCenter.z + 4.0f));
        _slug2 = Instantiate(slugObject);
        _slug2.GetComponent<SlugController>().ChangePos(new Vector2(roomCenter.x - 6.0f, roomCenter.z - 4.0f));
        _slug3 = Instantiate(slugObject);
        _slug3.GetComponent<SlugController>().ChangePos(new Vector2(roomCenter.x + 6.0f, roomCenter.z + 4.0f));
        _slug4 = Instantiate(slugObject);
        _slug4.GetComponent<SlugController>().ChangePos(new Vector2(roomCenter.x + 6.0f, roomCenter.z - 4.0f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyRoom()
    {
        Destroy(_slug1);
        Destroy(_slug2);
        Destroy(_slug3);
        Destroy(_slug4);

        Destroy(gameObject);
    }
}
