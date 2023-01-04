using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1 : MonoBehaviour
{
    [SerializeField]
    private GameObject batObject;

    [SerializeField]
    private GameObject movableBlockObject;

    private GameObject _bat1;
    private GameObject _bat2;

    private GameObject _PuzzleWall1;
    private GameObject _PuzzleWall2;

    private Vector3 roomCenter;


    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _bat1 = Instantiate(batObject);
        _bat1.GetComponent<BatController>().ChangePos(new Vector2(roomCenter.x - 6.0f, roomCenter.z + 0.0f));
        _bat2 = Instantiate(batObject);
        _bat2.GetComponent<BatController>().ChangePos(new Vector2(roomCenter.x + 10.0f, roomCenter.z - 3.0f));

        _PuzzleWall1 = Instantiate(movableBlockObject);
        _PuzzleWall1.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x - 0.5f,0f, roomCenter.z + 1.5f));
        _PuzzleWall2 = Instantiate(movableBlockObject);
        _PuzzleWall2.GetComponent<Rigidbody>().MovePosition(new Vector3(roomCenter.x + 0.5f, 0f, roomCenter.z + 1.5f));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyRoom()
    {
        Destroy(_bat1);
        Destroy(_bat2);
        Destroy(_PuzzleWall1);
        Destroy(_PuzzleWall2);

        Destroy(gameObject);
    }
}
