using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room11 : MonoBehaviour
{
    [SerializeField]
    private GameObject slugObject;

    [SerializeField]
    private GameObject skeletonObject;

    private GameObject _slug1;
    private GameObject _slug2;

    private GameObject _skeleton1;
    private GameObject _skeleton2;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _slug1 = Instantiate(slugObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z + 2.0f), Quaternion.identity);
        _slug2 = Instantiate(slugObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z - 2.0f), Quaternion.identity);
        _skeleton1 = Instantiate(skeletonObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z - 2.0f), Quaternion.identity);
        _skeleton2 = Instantiate(skeletonObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z + 2.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestroyRoom()
    {
        Destroy(_slug1);
        Destroy(_slug2);
        Destroy(_skeleton1);
        Destroy(_skeleton2);

        Destroy(gameObject);
    }
}
