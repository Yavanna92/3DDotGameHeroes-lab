using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5 : MonoBehaviour
{

    [SerializeField]
    private GameObject scorpionObject;

    [SerializeField]
    private GameObject skeletonObject;

    [SerializeField]
    private GameObject batObject;

    [SerializeField]
    private GameObject movableBlockObject;

    [SerializeField]
    private GameObject destroyableBlockObject;

    private GameObject _scorpion1;
    private GameObject _scorpion2;

    private GameObject _bat1;

    private GameObject _skeleton1;

    private GameObject _puzzleBlock1;
    private GameObject _puzzleBlock2;
    private GameObject _puzzleBlock3;
    private GameObject _puzzleBlock4;
    private GameObject _puzzleBlock5;
    private GameObject _puzzleBlock6;
    private GameObject _puzzleBlock7;
    private GameObject _puzzleBlock8;



    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {

        roomCenter = gameObject.GetComponent<Transform>().position;

        _scorpion1 = Instantiate(scorpionObject, new Vector3(roomCenter.x - 5.0f, 0.0f, roomCenter.z + 3.0f), Quaternion.identity);
        _scorpion2 = Instantiate(scorpionObject, new Vector3(roomCenter.x - 5.0f, 0.0f, roomCenter.z - 3.0f), Quaternion.identity);
        _skeleton1 = Instantiate(skeletonObject, new Vector3(roomCenter.x + 0.0f, 0.0f, roomCenter.z - 3.0f), Quaternion.identity);
        _bat1 = Instantiate(batObject, new Vector3(32.0f, 1.5f, roomCenter.z + 0.0f), Quaternion.identity);

        _puzzleBlock1 = Instantiate(destroyableBlockObject);
        _puzzleBlock1.GetComponent<DestroyableBlockController>().SetPosition(new Vector3(roomCenter.x + 2.5f, 0.0f, roomCenter.z - 1.5f));
        _puzzleBlock2 = Instantiate(destroyableBlockObject);
        _puzzleBlock2.GetComponent<DestroyableBlockController>().SetPosition(new Vector3(roomCenter.x + 2.5f, 0.0f, roomCenter.z - 2.5f));
        _puzzleBlock3 = Instantiate(destroyableBlockObject);
        _puzzleBlock3.GetComponent<DestroyableBlockController>().SetPosition(new Vector3(roomCenter.x + 3.5f, 0.0f, roomCenter.z - 1.5f));
        _puzzleBlock4 = Instantiate(destroyableBlockObject);
        _puzzleBlock4.GetComponent<DestroyableBlockController>().SetPosition(new Vector3(roomCenter.x + 3.5f, 0.0f, roomCenter.z - 2.5f));
        _puzzleBlock5 = Instantiate(movableBlockObject);
        _puzzleBlock5.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x + 1.5f, 0.0f, roomCenter.z - 0.5f));
        _puzzleBlock6 = Instantiate(movableBlockObject);
        _puzzleBlock6.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x + 1.5f, 0.0f, roomCenter.z + 0.5f));
        _puzzleBlock7 = Instantiate(movableBlockObject);
        _puzzleBlock7.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x - 2.5f, 0.0f, roomCenter.z + 3.5f));
        _puzzleBlock8 = Instantiate(movableBlockObject);
        _puzzleBlock8.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x - 2.5f, 0.0f, roomCenter.z + 4.5f));

    }

    public void DestroyRoom()
    {
        Destroy(_scorpion1);
        Destroy(_scorpion2);
        Destroy(_skeleton1);
        Destroy(_bat1);
        Destroy(_puzzleBlock1);
        Destroy(_puzzleBlock2);
        Destroy(_puzzleBlock3);
        Destroy(_puzzleBlock4);
        Destroy(_puzzleBlock5);
        Destroy(_puzzleBlock6);
        Destroy(_puzzleBlock7);
        Destroy(_puzzleBlock8);

        Destroy(gameObject);
    }
}
