using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room12 : MonoBehaviour
{

    [SerializeField]
    private GameObject slugObject;

    [SerializeField]
    private GameObject batObject;

    private GameObject _slug1;
    private GameObject _slug2;

    private GameObject _bat1;
    private GameObject _bat2;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _slug1 = Instantiate(slugObject, new Vector3(roomCenter.x + 1.0f, 0.0f, roomCenter.z + 4.0f), Quaternion.identity);
        _slug2 = Instantiate(slugObject, new Vector3(roomCenter.x + 1.0f, 0.0f, roomCenter.z - 4.0f), Quaternion.identity);
        _bat1 = Instantiate(batObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z + 4.0f), Quaternion.identity);
        _bat2 = Instantiate(batObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z - 4.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        Destroy(_slug1);
        Destroy(_slug2);
        Destroy(_bat1);
        Destroy(_bat2);
    }
}
