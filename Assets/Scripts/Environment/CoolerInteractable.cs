using TMPro;
using UnityEngine;

public class CoolerInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject drinkWaterArea;
    [SerializeField] private float interactionPriority = 5f;

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingWater)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingWater);
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
        }
    }

    public void ShowHint()
    {
        drinkWaterArea.SetActive(true);
    }
    public void HideHint()
    {
        drinkWaterArea.SetActive(false);
    }

    public float GetPriority() => interactionPriority;
}
