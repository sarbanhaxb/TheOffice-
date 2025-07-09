using TMPro;
using UnityEngine;

public class PresentationInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject presentPlaceHint;
    [SerializeField] private float interactionPriority = 5f;

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Present && PlayerStats.Instance.GetStressRatio() != 1f)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Present);
            presentPlaceHint.GetComponent<TMP_Text>().text = "Press E to start present";
        }
        else if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.Present && PlayerStats.Instance.GetStressRatio() == 1f)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Tired);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            presentPlaceHint.GetComponent<TMP_Text>().text = "Press E to stop present";
        }
    }

    public void ShowHint()
    {
        presentPlaceHint.SetActive(true);
    }
    public void HideHint()
    {
        presentPlaceHint.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
}
