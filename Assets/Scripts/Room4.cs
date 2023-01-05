using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4 : MonoBehaviour
{

    [SerializeField]
    private GameObject scorpionObject;

    private GameObject _scorpion1;
    private GameObject _scorpion2;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _scorpion1 = Instantiate(scorpionObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z + 0.0f), Quaternion.identity);
        _scorpion2 = Instantiate(scorpionObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z + 0.0f), Quaternion.identity);
    }

    public void DestroyRoom()
    {
        Destroy(_scorpion1);
        Destroy(_scorpion2);

        Destroy(gameObject);
    }
}
