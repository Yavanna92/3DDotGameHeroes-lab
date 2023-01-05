using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room9 : MonoBehaviour
{

    [SerializeField]
    private GameObject scorpionObject;

    [SerializeField]
    private GameObject slugObject;

    private GameObject _scorpion1;
    private GameObject _scorpion2;

    private GameObject _slug1;
    private GameObject _slug2;


    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _scorpion1 = Instantiate(scorpionObject, new Vector3(roomCenter.x - 2.0f, 0.0f, roomCenter.z + 4.0f), Quaternion.identity);
        _scorpion2 = Instantiate(scorpionObject, new Vector3(roomCenter.x + 2.0f, 0.0f, roomCenter.z + 4.0f), Quaternion.identity);
        _slug1 = Instantiate(slugObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z - 4.0f), Quaternion.identity);
        _slug2 = Instantiate(slugObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z - 4.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDestroy()
    {
        Destroy(_scorpion1);
        Destroy(_scorpion2);
        Destroy(_slug1);
        Destroy(_slug2);
    }
}
