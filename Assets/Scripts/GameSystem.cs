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

    private Button _startButton;

    private Button _menuButton;

    private RoomId _currentRoomId;

    private bool _isGameOver;

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

        _currentRoomId = RoomId.Entrance;

        _coinCounter = 0;
        _keyCounter = 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        switch (_currentRoomId)
        {
            case RoomId.Entrance:
                InitRoom();
                break;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentRoomId();
        UpdateCameraDebugKeys();
        _gameUiCanvas.GetComponent<GameUIController>().UpdateHealthBar(_player.GetComponent<PlayerHealth>().Health);
        
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
        _bat = Instantiate(_bat);
        _bat.GetComponent<BatDamage>().playerHealth = _player.GetComponent<PlayerHealth>();

        _skeleton = Instantiate(_skeleton);
        _skeleton.GetComponentInChildren<SkeletonDamage>().playerHealth = _player.GetComponent<PlayerHealth>();

        _slug = Instantiate(_slug);
        _slug.GetComponent<SlugDamage>().playerHealth = _player.GetComponent<PlayerHealth>();

        _scorpion = Instantiate(_scorpion);
        _scorpion.GetComponentInChildren<ScorpionHitDamage>().playerHealth = _player.GetComponent<PlayerHealth>();
    }

    private void UpdateCurrentRoomId()
    {
        var pos = _player.GetComponent<PlayerController>().PlayerPos;
        var hasChanged = false;

        // TODO: make adjustments to rooms boundaries as soon as we have the final Player model,
        // now they are not precise enough as Player center is at the mass center
        switch (_currentRoomId)
        {
            case RoomId.Entrance:
                if (pos.z >= -18.5f)
                {
                    _currentRoomId = RoomId.Room2;
                    _camera.GetComponent<CameraController>().MoveCamera(_currentRoomId);
                    hasChanged = true;
                }
                break;
            case RoomId.Room2:
                if (pos.z < -18.5f)
                {
                    _currentRoomId = RoomId.Room1;
                    
                    hasChanged = true;
                }
                if (pos.x <= -8.0f)
                {
                    _currentRoomId = RoomId.Room3;

                    hasChanged = true;
                }
                if (pos.z >= -5.5f)
                {
                    _currentRoomId = RoomId.Room6;

                    hasChanged = true;
                }
                break;
            case RoomId.Room3:
                if (pos.x > -8.0f)
                {
                    _currentRoomId = RoomId.Room2;

                    hasChanged = true;
                }
                if (pos.z >= -6.5f)
                {
                    _currentRoomId = RoomId.Room4;

                    hasChanged = true;
                }
                break;
            case RoomId.Room4:
                if (pos.z < -6.5f)
                {
                    _currentRoomId = RoomId.Room3;

                    hasChanged = true;
                }
                if (pos.x <= -24.5f)
                {
                    _currentRoomId = RoomId.Room5;

                    hasChanged = true;
                }
                if (pos.z >= 5.5f)
                {
                    _currentRoomId = RoomId.Room9;

                    hasChanged = true;
                }
                break;
            case RoomId.Room5:
                if (pos.x > -24.5f)
                {
                    _currentRoomId = RoomId.Room4;

                    hasChanged = true;
                }
                break;
            case RoomId.Room6:
                if (pos.z < -5.5f)
                {
                    _currentRoomId = RoomId.Room2;

                    hasChanged = true;
                }
                if (pos.x >= 8.5f)
                {
                    _currentRoomId = RoomId.Room7;

                    hasChanged = true;
                }
                break;
            case RoomId.Room7:
                if (pos.x < 8.5f)
                {
                    _currentRoomId = RoomId.Room6;

                    hasChanged = true;
                }
                if (pos.z <= -5.5f)
                {
                    _currentRoomId = RoomId.Room8;

                    hasChanged = true;
                }
                if (pos.x >= 24.0f)
                {
                    _currentRoomId = RoomId.Room12;

                    hasChanged = true;
                }
                if (pos.z >= 5.5f)
                {
                    _currentRoomId = RoomId.Room11;

                    hasChanged = true;
                }
                break;
            case RoomId.Room8:
                if (pos.z > -5.5f)
                {
                    _currentRoomId = RoomId.Room7;

                    hasChanged = true;
                }
                break;
            case RoomId.Room9:
                if (pos.z < 5.5f)
                {
                    _currentRoomId = RoomId.Room4;

                    hasChanged = true;
                }
                if (pos.x >= -8.0f)
                {
                    _currentRoomId = RoomId.Room10;

                    hasChanged = true;
                }
                break;
            case RoomId.Room10:
                if (pos.x < -8.0f)
                {
                    _currentRoomId = RoomId.Room9;

                    hasChanged = true;
                }
                if (pos.x >= 8.5f)
                {
                    _currentRoomId = RoomId.Room11;

                    hasChanged = true;
                }
                if (pos.z >= 17.5f)
                {
                    _currentRoomId = RoomId.BossRoom;

                    hasChanged = true;
                }
                break;
            case RoomId.Room11:
                if (pos.x < 8.5f)
                {
                    _currentRoomId = RoomId.Room10;

                    hasChanged = true;
                }
                if (pos.z < 5.5f)
                {
                    _currentRoomId = RoomId.Room7;

                    hasChanged = true;
                }
                break;
            case RoomId.Room12:
                if (pos.x < 24.0f)
                {
                    _currentRoomId = RoomId.Room7;

                    hasChanged = true;
                }
                break;
            case RoomId.Room13:
                if (pos.z < 17.5f)
                {
                    _currentRoomId = RoomId.Room10;

                    hasChanged = true;
                }
                break;
        }

        if (hasChanged)
            _camera.GetComponent<CameraController>().MoveCamera(_currentRoomId);
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

