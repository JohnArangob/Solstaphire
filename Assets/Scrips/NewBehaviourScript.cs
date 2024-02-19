using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject coinTexGO = GameObject.Find("Coin Text");
        coinText = coinTexGO.GetComponent<TextMeshProUGUI>();
    }
    
    // Update is called once per frame
    void Update()
    {
        coinText.text = coinCount.ToString();
    }   
}
