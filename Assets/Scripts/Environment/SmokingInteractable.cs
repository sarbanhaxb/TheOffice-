using TMPro;
using UnityEngine;

public class SmokingInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject smokeHint;

    [SerializeField] private float interactionPriority = 5f;

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Smoking)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Smoking);
            smokeHint.GetComponent<TMP_Text>().text = "Press E to stop";
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            smokeHint.GetComponent<TMP_Text>().text = "Press E to stress out";
        }

    }
    public void ShowHint()
    {
        smokeHint.SetActive(true);
    }
    public void HideHint()
    {
        smokeHint.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
}
