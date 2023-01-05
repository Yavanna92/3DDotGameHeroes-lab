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

        _skeleton1 = Instantiate(skeletonObject);
        _skeleton1.GetComponent<SkeletonController>().ChangePos(new Vector2(roomCenter.x - 6.0f, roomCenter.z + 0.0f));
        _skeleton2 = Instantiate(skeletonObject);
        _skeleton2.GetComponent<SkeletonController>().ChangePos(new Vector2(roomCenter.x + 6.0f, roomCenter.z + 0.0f));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyRoom()
    {
        Destroy(_skeleton1);
        Destroy(_skeleton2);

        Destroy(gameObject);
    }

}
