using Assets.Sources;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

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

    [SerializeField]
    private Light _ambientLight;

    [SerializeField]
    private GameObject _bat;

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

    [SerializeField]
    private GameObject doorObject;

    private GameObject _currentRoom;

    private GameObject _door1;
    private GameObject _door2;
    private GameObject _door3;
    private GameObject _door4;
    private GameObject _bossDoor;

    private Button _startButton;

    private Button _menuButton;

    private Button _instructionsButton;
    private Button _instMenuButton;
    private Button _creditsButton;
    private Button _credMenuButton;

    private RoomId _currentRoomId;

    private bool _isGameOver;

    private bool _hasBoomerang;

    private int _keyCounter;

    private GameObject _cleanRoom1;

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
        _startButton = _mainMenuCanvas.GetComponentsInChildren<GameObject>()[0].GetComponentsInChildren<Button>()[0];
        _instructionsButton = _mainMenuCanvas.GetComponentsInChildren<GameObject>()[0].GetComponentsInChildren<Button>()[1];
        _creditsButton = _mainMenuCanvas.GetComponentsInChildren<GameObject>()[0].GetComponentsInChildren<Button>()[2];
        _instMenuButton = _mainMenuCanvas.GetComponentsInChildren<GameObject>()[1].GetComponentInChildren<Button>();
        _creditsButton = _credMenuButton.GetComponentsInChildren<GameObject>()[2].GetComponentInChildren<Button>();

        _currentRoomId = RoomId.Entrance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _cleanRoom1 = room1_Object;
        _player = Instantiate(_player);

        _currentRoom = Instantiate(room1_Object);

        _chest = Instantiate(_chest, new Vector3(1.5f, 0.0f, 0.5f), Quaternion.identity);

        _door1 = Instantiate(doorObject, new Vector3(0.0f, 0.0f, -6.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        _door2 = Instantiate(doorObject, new Vector3(-16.0f, 0.0f, 6.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        _door3 = Instantiate(doorObject, new Vector3(16.0f, 0.0f, 6.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
        _door4 = Instantiate(doorObject, new Vector3(24.0f, 0.0f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
        _bossDoor = Instantiate(doorObject, new Vector3(0.0f, 0.0f, 18.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentRoomId();
        UpdateCameraDebugKeys();
        _gameUiCanvas.GetComponent<GameUIController>().UpdateHealthBar(_player.GetComponent<PlayerHealth>().Health);

            if (_keyCounter < 4 && Input.GetKeyDown(KeyCode.K))
            {
                if (_keyCounter == 0) _door1.gameObject.GetComponent<Animator>().Play("Open");
                if (_keyCounter == 1) _door2.gameObject.GetComponent<Animator>().Play("Open");
                if (_keyCounter == 2)
                {
                    _door3.gameObject.GetComponent<Animator>().Play("Open");
                    _door4.gameObject.GetComponent<Animator>().Play("Open");
                }
                if (_keyCounter == 3) _bossDoor.gameObject.GetComponent<Animator>().Play("Open");
                _keyCounter += 1;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                _door1.gameObject.GetComponent<Animator>().Play("Open");
                _door2.gameObject.GetComponent<Animator>().Play("Open");
                _door3.gameObject.GetComponent<Animator>().Play("Open");
                _door4.gameObject.GetComponent<Animator>().Play("Open");
                _bossDoor.gameObject.GetComponent<Animator>().Play("Open");
            }

        if (!_hasBoomerang)
        {
            if (_chest.GetComponent<ActivationController>().isActive())
            {
                _player.GetComponent<PlayerController>()._hasBoomerang = true;
                _gameUiCanvas.GetComponent<GameUIController>().ActivateBoomerang();
                _hasBoomerang = true;
            }
        }

        if (_isGameOver)
        {
            _gameUiCanvas.GetComponent<GameUIController>().ShowGameOver();
            _menuButton.onClick.AddListener(ShowMenu);
        }
        else
            _isGameOver = _player.GetComponent<PlayerHealth>().Health == 0 && !_isGameOver;
    }

    private void OnEnable()
    {
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[0].SetActive(true);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[1].SetActive(false);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[2].SetActive(false);
        _startButton.onClick.AddListener(StartGame);
        _instructionsButton.onClick.AddListener(displayInstructions);
        _creditsButton.onClick.AddListener(displayCredits);
        _menuButton.onClick.RemoveListener(ShowMenu);
        _instMenuButton.onClick.RemoveListener(ShowMenu);
        _credMenuButton.onClick.RemoveListener(ShowMenu);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void ShowMenu()
    {
        Time.timeScale = 0f;

        _gameUiCanvas.GetComponent<GameUIController>().HideGameOver();

        _startButton.gameObject.SetActive(true);
        _mainMenuCanvas.gameObject.SetActive(true);
    }

    private void displayInstructions()
    {
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[0].SetActive(false);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[1].SetActive(true);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[2].SetActive(false);
        _startButton.onClick.RemoveListener(StartGame);
        _instructionsButton.onClick.RemoveListener(displayInstructions);
        _creditsButton.onClick.RemoveListener(displayCredits);
        _instMenuButton.onClick.AddListener(ShowMenu);
    }

    private void displayCredits()
    {
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[0].SetActive(false);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[1].SetActive(false);
        _gameUiCanvas.GetComponentsInChildren<GameObject>()[2].SetActive(true);
        _startButton.onClick.RemoveListener(StartGame);
        _instructionsButton.onClick.RemoveListener(displayInstructions);
        _creditsButton.onClick.RemoveListener(displayCredits);
        _credMenuButton.onClick.AddListener(ShowMenu);
    }

    private void StartGame()
    {
        if (_isGameOver)
        {
            if (_currentRoomId == RoomId.Entrance)
            {
                Destroy(_currentRoom);
                _currentRoom = Instantiate(_cleanRoom1);
            }
                
            _currentRoomId = RoomId.Entrance;

            UpdateCurrentRoomId();

            _chest.GetComponent<ActivationController>().Activate();


            _door1.GetComponent<DoorController>().gameObject.SetActive(true);
            _door1.GetComponent<Animator>().Play("Close");

            _door2.GetComponent<DoorController>().gameObject.SetActive(true);
            _door2.GetComponent<Animator>().Play("Close");

            _door3.GetComponent<DoorController>().gameObject.SetActive(true);
            _door3.GetComponent<Animator>().Play("Close");

            _door4.GetComponent<DoorController>().gameObject.SetActive(true);
            _door4.GetComponent<Animator>().Play("Close");

            _bossDoor.GetComponent<DoorController>().gameObject.SetActive(true);
            _bossDoor.GetComponent<Animator>().Play("Close");

            _gameUiCanvas.GetComponent<GameUIController>().HideGameOver();
        }

        _player.GetComponent<PlayerHealth>().ResetHealth();
        _player.GetComponent<PlayerController>().ResetPos();

        _hasBoomerang = false;

        _keyCounter = 0;

        _currentRoomId = RoomId.Entrance;
        _isGameOver = false;

        // Hides the button
        _startButton.gameObject.SetActive(false);
        _mainMenuCanvas.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }


    void OnGui()
    {
        // common GUI code goes here
    }


    private void UpdateCurrentRoomId()
    {
        var pos = _player.GetComponent<PlayerController>().PlayerPos;
        var hasChanged = false;
        var roomCenter = _currentRoom.transform.position;

        // TODO: make adjustments to rooms boundaries as soon as we have the final Player model,
        // now they are not precise enough as Player center is at the mass center
        switch (_currentRoomId)
        {
            case RoomId.Entrance:
                if (pos.z >= roomCenter.z + 7.0f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;
                    _camera.GetComponent<CameraController>().MoveCamera(_currentRoomId);
                    hasChanged = true;
                }
                break;
            case RoomId.Room2:
                if (pos.z < roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room1;
                    _currentRoom = room1_Object;

                    hasChanged = true;
                }
                if (pos.x <= roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room3;
                    _currentRoom = room3_Object;

                    hasChanged = true;
                }
                if (pos.z >= roomCenter.z + 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room6;
                    _currentRoom = room6_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room3:
                if (pos.x > roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;

                    hasChanged = true;
                }
                if (pos.z >= roomCenter.z + 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room4:
                if (pos.z < roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room3;
                    _currentRoom = room3_Object;

                    hasChanged = true;
                }
                if (pos.x <= roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room5;
                    _currentRoom = room5_Object;

                    hasChanged = true;
                }
                if (pos.z >= roomCenter.z + 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room9;
                    _currentRoom = room9_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room5:
                if (pos.x > roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room6:
                if (pos.z < roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room2;
                    _currentRoom = room2_Object;

                    hasChanged = true;
                }
                if (pos.x >= roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room7:
                if (pos.x < roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room6;
                    _currentRoom = room6_Object;

                    hasChanged = true;
                }
                if (pos.z <= roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room8;
                    _currentRoom = room8_Object;

                    hasChanged = true;
                }
                if (pos.x >= roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room12;
                    _currentRoom = room12_Object;

                    hasChanged = true;
                }
                if (pos.z >= roomCenter.z + 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room11;
                    _currentRoom = room11_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room8:
                if (pos.z > roomCenter.z + 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room9:
                if (pos.z < roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room4;
                    _currentRoom = room4_Object;

                    hasChanged = true;
                }
                if (pos.x >= roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room10:
                if (pos.x < roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room9;
                    _currentRoom = room9_Object;

                    hasChanged = true;
                }
                if (pos.x >= roomCenter.x + 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room11;
                    _currentRoom = room11_Object;

                    hasChanged = true;
                }
                if (pos.z >= roomCenter.z + 7f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.BossRoom;
                    _currentRoom = bossRoom_Object;
                    _bossDoor.GetComponent<DoorController>().gameObject.SetActive(true);
                    _bossDoor.GetComponent<Animator>().Play("Close");

                    hasChanged = true;
                }
                break;
            case RoomId.Room11:
                if (pos.x < roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                if (pos.z < roomCenter.z - 6.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room12:
                if (pos.x < roomCenter.x - 8.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room7;
                    _currentRoom = room7_Object;

                    hasChanged = true;
                }
                break;
            case RoomId.Room13:
                if (pos.z < roomCenter.z - 9.5f)
                {
                    Destroy(_currentRoom);
                    _currentRoomId = RoomId.Room10;
                    _currentRoom = room10_Object;

                    hasChanged = true;
                }
                break;
            default:
                Destroy(_currentRoom);
                _currentRoomId = RoomId.Room1;
                _currentRoom = room1_Object;
                hasChanged = true;
                break;
        }

        if (hasChanged)
        {

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

