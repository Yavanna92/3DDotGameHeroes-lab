using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int Coins { get; private set; }
    private TextMeshProUGUI _cc;

    // Start is called before the first frame update
    void Start()
    {
        Coins = 0;
        _cc = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _cc.text = "x " + Coins.ToString();
    }

    public void AddCoin()
    {
        Coins++;
    }

    public void ResetCoins()
    {
        Coins = 0;
    }


}
