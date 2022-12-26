using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        // FOV and Clipping Planes are set camera's prefab properties
        _camera = GetComponent<Camera>();

        // Init camera
        MoveCamera(RoomId.Entrance);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateCameraDebugKeys();
    }

    public void MoveCamera(RoomId roomId)
    {
        if (roomId != RoomId.BossRoom)
        {
            _camera.transform.rotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);
        }
        // A slightly greater angle is used on Boss Room to minimize invisible areas
        // and display a pattern of hidden tiles similarly to the other rooms (~2 tiles)
        else
        {
            _camera.transform.rotation = Quaternion.Euler(50.0f, 0.0f, 0.0f);
        }

        // Don't change these values
        switch (roomId)
        {
            case RoomId.Room1:
                _camera.transform.position = new Vector3(0.0f, 8.0f, -32.25f);
                break;
            case RoomId.Room2:
                _camera.transform.position = new Vector3(0.0f, 8.0f, -20.25f);
                break;
            case RoomId.Room3:
                _camera.transform.position = new Vector3(-16.0f, 8.0f, -20.25f);
                break;
            case RoomId.Room4:
                _camera.transform.position = new Vector3(-16.0f, 8.0f, -8.25f);
                break;
            case RoomId.Room5:
                _camera.transform.position = new Vector3(-32.0f, 8.0f, -8.25f);
                break;
            case RoomId.Room6:
                _camera.transform.position = new Vector3(-0.0f, 8.0f, -8.25f);
                break;
            case RoomId.Room7:
                _camera.transform.position = new Vector3(16.0f, 8.0f, -8.25f);
                break;
            case RoomId.Room8:
                _camera.transform.position = new Vector3(16.0f, 8.0f, -20.25f);
                break;
            case RoomId.Room9:
                _camera.transform.position = new Vector3(-16.0f, 8.0f, 3.75f);
                break;
            case RoomId.Room10:
                _camera.transform.position = new Vector3(0.0f, 8.0f, 3.75f);
                break;
            case RoomId.Room11:
                _camera.transform.position = new Vector3(16.0f, 8.0f, 3.75f);
                break;
            case RoomId.Room12:
                _camera.transform.position = new Vector3(32.0f, 8.0f, -8.25f);
                break;
            case RoomId.BossRoom:
                _camera.transform.position = new Vector3(0.0f, 13.33f, 14.5f);
                break;
        }

    }

    //private void UpdateCameraDebugKeys()
    //{
    //    if (Input.GetKey(KeyCode.F1))
    //        _camera.transform.position = new Vector3(0.0f, 8.0f, -32.0f);

    //    if (Input.GetKey(KeyCode.F2))
    //        _camera.transform.position = new Vector3(0.0f, 8.0f, -20.0f);

    //    if (Input.GetKey(KeyCode.F3))
    //        _camera.transform.position = new Vector3(-16.0f, 8.0f, -20.0f);

    //    if (Input.GetKey(KeyCode.F4))
    //        _camera.transform.position = new Vector3(-16.0f, 8.0f, -8.0f);

    //    if (Input.GetKey(KeyCode.F5))
    //        _camera.transform.position = new Vector3(-32.0f, 8.0f, -8.0f);

    //    if (Input.GetKey(KeyCode.F6))
    //        _camera.transform.position = new Vector3(0.0f, 8.0f, -8.0f);

    //    if (Input.GetKey(KeyCode.F7))
    //        _camera.transform.position = new Vector3(-32.0f, 8.0f, -8.0f);

    //    if (Input.GetKey(KeyCode.F8))
    //        _camera.transform.position = new Vector3(16.0f, 8.0f, -20.0f);

    //    if (Input.GetKey(KeyCode.F9))
    //        _camera.transform.position = new Vector3(-16.0f, 8.0f, 4.0f);

    //    if (Input.GetKey(KeyCode.F10))
    //        _camera.transform.position = new Vector3(0.0f, 8.0f, 4.0f);

    //    if (Input.GetKey(KeyCode.F11))
    //        _camera.transform.position = new Vector3(16.0f, 8.0f, 4.0f);

    //    if (Input.GetKey(KeyCode.F12))
    //        _camera.transform.position = new Vector3(32.0f, 8.0f, -8.0f);

    //    if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F1))
    //        _camera.transform.position = new Vector3(0.0f, 8.0f, 16.0f);
    //}
}
