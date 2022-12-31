using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;
    // Start is called before the first frame update
    void Start()
    {
        _startButton.GetComponent<Button>();
    }

    private void OnEnable()
    {
        //_gameObject.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        //_startButton.onClick.RemoveListener(StartGame);
    }
}
