using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorkingInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject workPlaceHint;
    [SerializeField] private float interactionPriority = 5f;
    [SerializeField] private Canvas screen;

    private void Awake()
    {
        screen = GameObject.FindGameObjectWithTag("Screen").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (GameTime.Instance.GetCurrentDayPart() == DayPart.evening && PlayerCurrentState.Instance.GetCurrentState() == PlayerStates.Working)
        {
            screen.enabled = false;
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Working && PlayerStats.Instance.GetStressRatio() != 1f && GameTime.Instance.GetCurrentDayPart() == DayPart.morning)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Working);
            screen.enabled = true;
            workPlaceHint.GetComponent<TMP_Text>().text = "Press E to stop working";
        }
        else if ((PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Working && PlayerStats.Instance.GetStressRatio() == 1f) || GameTime.Instance.GetCurrentDayPart() != DayPart.morning)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Tired);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            workPlaceHint.GetComponent<TMP_Text>().text = "Press E to start work";
            screen.enabled = false;
        }
    }

    public void ShowHint()
    {
        workPlaceHint.SetActive(true);
    }
    public void HideHint()
    {
        workPlaceHint.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
}
