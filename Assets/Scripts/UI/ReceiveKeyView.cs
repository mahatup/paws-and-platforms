using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReceiveKeyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _notificationText;

    public void ClearText()
    {
        if (_notificationText != null)
            _notificationText.text = string.Empty;
    }
}
