using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CoinCounterView : BaseView
    {
        [SerializeField] private TMP_Text _counterCoinText;

        public void SetCounterCoin(int counterCoin)
        {
            _counterCoinText.text = counterCoin.ToString();
        }
    }
}