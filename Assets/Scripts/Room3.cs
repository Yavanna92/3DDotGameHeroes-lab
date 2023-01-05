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

        _slug1 = Instantiate(slugObject, new Vector3(roomCenter.x - 6.0f, 0f, roomCenter.z + 4.0f), Quaternion.identity);
        _slug2 = Instantiate(slugObject, new Vector3(roomCenter.x - 6.0f, 0f, roomCenter.z - 4.0f), Quaternion.identity);
        _slug3 = Instantiate(slugObject, new Vector3(roomCenter.x + 6.0f, 0f, roomCenter.z + 4.0f), Quaternion.identity);
        _slug4 = Instantiate(slugObject, new Vector3(roomCenter.x + 6.0f, 0f, roomCenter.z - 4.0f), Quaternion.identity);
    }

    public void OnDestroy()
    {
        Destroy(_slug1);
        Destroy(_slug2);
        Destroy(_slug3);
        Destroy(_slug4);
    }
}
