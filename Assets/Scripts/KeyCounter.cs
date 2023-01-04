using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{

    public int Keys { get; private set; }
    private TextMeshProUGUI _kc;

    // Start is called before the first frame update
    void Start()
    {
        Keys = 0;
        _kc = gameObject.GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        _kc.text = "x " + Keys.ToString();
    }

    public void AddKey()
    {
        Keys++;
    }

    public void ResetKey()
    {
        Keys = 0;
    }

}
