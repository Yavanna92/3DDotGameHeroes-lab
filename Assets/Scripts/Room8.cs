using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room8 : MonoBehaviour
{

    [SerializeField]
    private GameObject scorpionObject;

    [SerializeField]
    private GameObject skeletonObject;

    private GameObject _scorpion1;

    private GameObject _skeleton1;
    private GameObject _skeleton2;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _scorpion1 = Instantiate(scorpionObject, new Vector3(roomCenter.x - 0.0f, 0.0f, roomCenter.z + 1.0f), Quaternion.identity);
        _skeleton1 = Instantiate(skeletonObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z - 3.0f), Quaternion.identity);
        _skeleton2 = Instantiate(skeletonObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z - 3.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyRoom()
    {
        Destroy(_scorpion1);
        Destroy(_skeleton1);
        Destroy(_skeleton2);

        Destroy(gameObject);
    }
}
