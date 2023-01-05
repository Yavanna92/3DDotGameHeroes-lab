using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room6 : MonoBehaviour
{
    [SerializeField]
    private GameObject movableBlockObject;

    [SerializeField] 
    private GameObject skeletonObject;

    [SerializeField]
    private GameObject slugObject;

    private GameObject _PuzzleWall1;
    private GameObject _PuzzleWall2;

    private GameObject _skeleton1;
    private GameObject _skeleton2;

    private GameObject _slug1;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _PuzzleWall1 = Instantiate(movableBlockObject);
        _PuzzleWall1.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x - 1.5f, 0f, roomCenter.z - 2.5f));
        _PuzzleWall2 = Instantiate(movableBlockObject);
        _PuzzleWall2.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x - 2.5f, 0f, roomCenter.z - 2.5f));
        _skeleton1 = Instantiate(skeletonObject, new Vector3(roomCenter.x + 6.0f, 0.0f, roomCenter.z + 3.0f), Quaternion.identity);
        _skeleton2 = Instantiate(skeletonObject, new Vector3(roomCenter.x - 6.0f, 0.0f, roomCenter.z + 3.0f), Quaternion.identity);
        _slug1 = Instantiate(slugObject, new Vector3(roomCenter.x - 1.0f, 0.0f, roomCenter.z + 0.0f), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        Destroy(_PuzzleWall1);
        Destroy(_PuzzleWall2);
        Destroy(_skeleton1);
        Destroy(_skeleton2);
        Destroy(_slug1);
    }
}
