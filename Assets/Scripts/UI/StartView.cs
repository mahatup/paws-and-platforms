using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StartView : BaseView
    {
        [SerializeField] private TMP_Text _loreText;
        [SerializeField] private List<string> _loreLines;

        public List<string> LoreLines => _loreLines;

        public void SetLoreText(string loreText)
        {
            _loreText.text = loreText;
        }
    }
}