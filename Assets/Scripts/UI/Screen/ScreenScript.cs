using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenScript : MonoBehaviour
{
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
