using Assets._Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    //public GameObject player;

    [SerializeField] 
    private GameObject _player;

    [SerializeField]
    private GameObject _map;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Light _ambientLight;

    [SerializeField]
    private GameObject _bat;

    private RoomId _currentRoomId;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;

        Instantiate(_map);
        Instantiate(_camera);
        Instantiate(_player);
        Instantiate(_ambientLight);
        _currentRoomId = RoomId.Entrance;
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (_currentRoomId)
        {
            case RoomId.Entrance:
                initRoom();
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGui()
    {
        // common GUI code goes here
    }

    void initRoom()
    {
        Instantiate(_bat);
    }
}

