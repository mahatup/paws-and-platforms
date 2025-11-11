using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounterView : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterCoinText;

    public void SetCounterCoin(int counterCoin)
    {
        _counterCoinText.text = counterCoin.ToString();
    }

}
