using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ReceiveKeyView : BaseView
    {
        [SerializeField] private TMP_Text _notificationText;

        public void ClearText()
        {
            if (_notificationText != null)
                _notificationText.text = string.Empty;
        }
    }
}