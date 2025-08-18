using System;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class ScreenScript : MonoBehaviour
{
    [SerializeField] private Button Chrome;
    [SerializeField] private Button Outlook;
    [SerializeField] private TMP_Text Timer;

    bool isOpen = true;

    private void Update()
    {
        if (isOpen)
        {
            Timer.text = GameTime.Instance.GetGameTime().ToString("HH:mm");
        }
    }

}
