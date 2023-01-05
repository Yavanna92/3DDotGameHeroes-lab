using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room10 : MonoBehaviour
{

    [SerializeField] private GameObject _bat;

    private Vector3 roomCenter;

    // Start is called before the first frame update
    void Start()
    {
        roomCenter = gameObject.GetComponent<Transform>().position;

        _bat = Instantiate(_bat, new Vector3(roomCenter.x, 1.5f, roomCenter.z), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDestroy()
    {
        Destroy(_bat);
    }
}
