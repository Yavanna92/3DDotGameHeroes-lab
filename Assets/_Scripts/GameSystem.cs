using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    public GameObject player;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGui()
    {
        // common GUI code goes here
    }
}

