using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2 : MonoBehaviour
{

    [SerializeField]
    private GameObject skeletonObject;

    [SerializeField]
    private GameObject movableBlockObject;

    private GameObject _skeleton1;
    private GameObject _skeleton2;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _skeleton1 = Instantiate(skeletonObject, new Vector3(roomCenter.x - 6.0f, 0f, roomCenter.z + 0.0f), Quaternion.identity);

        _skeleton2 = Instantiate(skeletonObject, new Vector3(roomCenter.x + 6.0f, 0f, roomCenter.z + 0.0f), Quaternion.identity);
    }

    public void OnDestroy()
    {
        Destroy(_skeleton1);
        Destroy(_skeleton2);
    }

}
