using Assets.Sources;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    [SerializeField] 
    private GameObject _player;

    [SerializeField]
    private GameObject _map;

    [SerializeField]
    private GameObject _camera;

    //Rooms
    [SerializeField]
    private GameObject room1_Object;
    [SerializeField]
    private GameObject room2_Object;
    [SerializeField]
    private GameObject room3_Object;
    [SerializeField]
    private GameObject room4_Object;
    [SerializeField]
    private GameObject room5_Object;
    [SerializeField]
    private GameObject room6_Object;
    [SerializeField]
    private GameObject room7_Object;
    [SerializeField]
    private GameObject room8_Object;
    [SerializeField]
    private GameObject room9_Object;
    [SerializeField]
    private GameObject room10_Object;
    [SerializeField]
    private GameObject room11_Object;
    [SerializeField]
    private GameObject room12_Object;

    [SerializeField]
    private GameObject bossRoom_Object;
    

    private GameObject _currentRoom;


    [SerializeField]
    private Light _ambientLight;

    [SerializeField]
    private GameObject _bat;
    private GameObject _bat2;

    [SerializeField]
    private GameObject _skeleton;

    [SerializeField]
    private GameObject _slug;

    [SerializeField]
    private GameObject _scorpion;

    [SerializeField]
    private Canvas _gameUiCanvas;

    [SerializeField]
    private Canvas _mainMenuCanvas;

    [SerializeField]
    private GameObject _chest;

    private Button _startButton;

    private Button _menuButton;

    private RoomId _currentRoomId;

    private bool _isGameOver;

    private bool _hasBoomerang;

    private int _coinCounter;
    private int _keyCounter;

    // When instantiating it is strictly necessary to save the instance in the private fields
    // if we need to access to any of the object components
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;

        Time.timeScale = 0f;

        // Instantiate components
        Instantiate(_map);
        _camera = Instantiate(_camera);
        
        Instantiate(_ambientLight);
        _gameUiCanvas = Instantiate(_gameUiCanvas);
        _menuButton = _gameUiCanvas.GetComponentInChildren<Button>();

        _mainMenuCanvas = Instantiate(_mainMenuCanvas);
        _startButton = _mainMenuCanvas.GetComponentInChildren<Button>();

        _player = Instantiate(_player);

        _chest = Instantiate(_chest, new Vector3(1.5f, 0.0f, 0.5f), Quaternion.identity);

        _currentRoomId = RoomId.Entrance;


    }

    // Start is called before the first frame update
    void Start()
    {
        InitRoom();
        _currentRoom = Instantiate(room1_Object);

        _hasBoomerang = false;

        _coinCounter = 0;
        _keyCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentRoomId();
        UpdateCameraDebugKeys();
        _gameUiCanvas.GetComponent<GameUIController>().UpdateHealthBar(_player.GetComponent<PlayerHealth>().Health);
        
        if (!_hasBoomerang)
        {
            if (_chest.GetComponent<ActivationController>().isActive())
            {
                _player.GetComponent<PlayerController>()._hasBoomerang = true;
                _gameUiCanvas.GetComponent<GameUIController>().ActivateBoomerang();
                _hasBoomerang=true;
            }
        }

        if (_isGameOver) {
            _gameUiCanvas.GetComponent<GameUIController>().ShowGameOver();
            _menuButton.onClick.AddListener(ShowMenu);
        }
        else
            _isGameOver = _player.GetComponent<PlayerHealth>().Health == 0 && !_isGameOver;
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _menuButton.onClick.RemoveListener(ShowMenu);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        _player.GetComponent<PlayerHealth>().ResetHealth();

        _currentRoomId = RoomId.Entrance;
        _isGameOver = false;
        // Hides the button
        _startButton.gameObject.SetActive(false);
        _mainMenuCanvas.gameObject.SetActive(false);

        _gameUiCanvas.GetComponent<GameUIController>().HideGameOver();

        Time.timeScale = 1f;
    }

    private void ShowMenu()
    {
        Time.timeScale = 0f;

        _gameUiCanvas.GetComponent<GameUIController>().HideGameOver();

        _startButton.gameObject.SetActive(true);
        _mainMenuCanvas.gameObject.SetActive(true);
    }

    void OnGui()
    {
        // common GUI code goes here
    }

    void InitRoom()
    {
        //_bat = Instantiate(_bat);

        //_bat = Instantiate(_bat);
        //_bat2.transform.position = new Vector3(1.0f, 1.5f, 0f);
        //_bat.GetComponent<BatController>().ChangePos(new Vector2(0.0f, -20.0f));

        //_skeleton = Instantiate(_skeleton);
        //_skeleton.GetComponentInChildren<SkeletonDamage>().playerHealth = _player.GetComponent<PlayerHealth>();

        //_slug = Instantiate(_slug);
        //_slug.GetComponent<SlugDamage>().playerHealth = _player.GetComponent<PlayerHealth>();

        //_scorpion = Instantiate(_scorpion);
        //_scorpion.GetComponentInChildren<ScorpionHitDamage>().playerHealth = _player.GetComponent<PlayerHealth>();
    }

    private void UpdateCurrentRoomId()
    {
        var pos = _player.GetComponent<PlayerController>().PlayerPos;
        var hasChanged = false;
        var previousRoom = _currentRoom;
        var roomCenter = _currentRoom.transform.position;

        // TODO: make adjustments to rooms boundaries as soon as we have the final Player model,
        // now they are not precise enough as Player center is at the mass center
        switch (_currentRoomId)
        {
            case RoomId.Entrance:
                if (pos.z >= roomCenter.z + 7.0f)
                {
                    _currentRoom.GetComponent<Room1>().DestroyRoom();
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;
                    _camera.GetComponent<CameraController>().MoveCamera(_currentRoomId);
                    hasChanged = true;
                }
                break;
            case RoomId.Room2:
                if (pos.z < roomCenter.z - 6.5f)
                {
                    _currentRoom.GetComponent<Room2>().DestroyRoom();
                    _currentRoomId = RoomId.Room1;
                    _currentRoom = room1_Object;

                    hasChanged = true;
                }
                if (pos.x <= roomCenter.x - 8.5f)
                {
                    _currentRoom.GetComponent<Room2>().DestroyRoom();
                    _currentRoomId = RoomId.Room3;
                    _currentRoom = room3_Object;

                    hasChanged = true;
                }
                if (pos.z >= -5.5f)
                {
                    _currentRoom.GetComponent<Room2>().DestroyRoom();
                    _currentRoomId = RoomId.Room6;
                    _currentRoom = room6_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room3:
                if (pos.x > -8.0f)
                {
                    _currentRoom.GetComponent<Room3>().DestroyRoom();
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;

                    hasChanged = true;
                }
                if (pos.z >= -6.5f)
                {
                    _currentRoom.GetComponent<Room3>().DestroyRoom();
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room4:
                if (pos.z < -6.5f)
                {
                    _currentRoom.GetComponent<Room4>().DestroyRoom();
                    _currentRoomId = RoomId.Room3;
                    _currentRoom = room3_Object;

                    hasChanged = true;
                }
                if (pos.x <= -24.5f)
                {
                    _currentRoom.GetComponent<Room4>().DestroyRoom();
                    _currentRoomId = RoomId.Room5;
                    _currentRoom = room5_Object;

                    hasChanged = true;
                }
                if (pos.z >= 5.5f)
                {
                    _currentRoom.GetComponent<Room4>().DestroyRoom();
                    _currentRoomId = RoomId.Room9;
                    _currentRoom = room9_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room5:
                if (pos.x > -24.5f)
                {
                    _currentRoom.GetComponent<Room5>().DestroyRoom();
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room6:
                if (pos.z < -5.5f)
                {
                    _currentRoom.GetComponent<Room6>().DestroyRoom();
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;

                    hasChanged = true;
                }
                if (pos.x >= 8.5f)
                {
                    _currentRoom.GetComponent<Room6>().DestroyRoom();
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room7:
                if (pos.x < 8.5f)
                {
                    _currentRoom.GetComponent<Room7>().DestroyRoom();
                    _currentRoomId = RoomId.Room6;
                    _currentRoom = room6_Object;

                    hasChanged = true;
                }
                if (pos.z <= -5.5f)
                {
                    _currentRoom.GetComponent<Room7>().DestroyRoom();
                    _currentRoomId = RoomId.Room8;
                    _currentRoom = room6_Object;

                    hasChanged = true;
                }
                if (pos.x >= 24.0f)
                {
                    _currentRoom.GetComponent<Room7>().DestroyRoom();
                    _currentRoomId = RoomId.Room12;
                    _currentRoom = room12_Object;

                    hasChanged = true;
                }
                if (pos.z >= 5.5f)
                {
                    _currentRoom.GetComponent<Room7>().DestroyRoom();
                    _currentRoomId = RoomId.Room11;
                    _currentRoom = room11_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room8:
                if (pos.z > -5.5f)
                {
                    _currentRoom.GetComponent<Room8>().DestroyRoom();
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room9:
                if (pos.z < 5.5f)
                {
                    _currentRoom.GetComponent<Room9>().DestroyRoom();
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                if (pos.x >= -8.0f)
                {
                    _currentRoom.GetComponent<Room9>().DestroyRoom();
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room10:
                if (pos.x < -8.0f)
                {
                    _currentRoom.GetComponent<Room10>().DestroyRoom();
                    _currentRoomId = RoomId.Room9;
                    _currentRoom = room9_Object;

                    hasChanged = true;
                }
                if (pos.x >= 8.5f)
                {
                    _currentRoom.GetComponent<Room10>().DestroyRoom();
                    _currentRoomId = RoomId.Room11;
                    _currentRoom = room11_Object;

                    hasChanged = true;
                }
                if (pos.z >= 17.5f)
                {
                    _currentRoom.GetComponent<Room10>().DestroyRoom();
                    _currentRoomId = RoomId.BossRoom;
                    _currentRoom = bossRoom_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room11:
                if (pos.x < 8.5f)
                {
                    _currentRoom.GetComponent<Room11>().DestroyRoom();
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                if (pos.z < 5.5f)
                {
                    _currentRoom.GetComponent<Room11>().DestroyRoom();
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room12:
                if (pos.x < 24.0f)
                {
                    _currentRoom.GetComponent<Room12>().DestroyRoom();
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room13:
                if (pos.z < 17.5f)
                {
                    _currentRoom.GetComponent<BossRoom>().DestroyRoom();
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                break;
        }

        if (hasChanged)
        {

            Destroy(previousRoom);
            _currentRoom = Instantiate(_currentRoom);

            _camera.GetComponent<CameraController>().MoveCamera(_currentRoomId);
        }
    }

    private void UpdateCameraDebugKeys()
    {
        var hasChanged = false;
        var newRoomId = RoomId.Entrance;

        if (Input.GetKey(KeyCode.F1))
        {
            newRoomId = RoomId.Room1;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F2))
        {
            newRoomId = RoomId.Room2;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F3))
        {
            newRoomId = RoomId.Room3;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F4))
        {
            newRoomId = RoomId.Room4;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F5))
        {
            newRoomId = RoomId.Room5;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F6))
        {
            newRoomId = RoomId.Room6;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F7))
        {
            newRoomId = RoomId.Room7;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F8))
        {
            newRoomId = RoomId.Room8;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F9))
        {
            newRoomId = RoomId.Room9;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F10))
        {
            newRoomId = RoomId.Room10;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F11))
        {
            newRoomId = RoomId.Room11;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.F12))
        {
            newRoomId = RoomId.Room12;
            hasChanged = true;
        }

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.F1))
        {
            newRoomId = RoomId.BossRoom;
            hasChanged = true;
        }

        if (hasChanged)
            _camera.GetComponent<CameraController>().MoveCamera(newRoomId);
    }
}

