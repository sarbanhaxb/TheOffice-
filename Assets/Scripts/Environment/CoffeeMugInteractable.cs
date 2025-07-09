using UnityEngine;

public class CoffeeMugInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    [SerializeField] private GameObject drinkCoffeeArea;
    [SerializeField] private float interactionPriority = 10f;

    public float GetPriority() => interactionPriority;
    public void Interact()
    {
        PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingCoffee);
        Destroy(gameObject);
    }
    public void ShowHint() => drinkCoffeeArea.SetActive(true);
    public void HideHint() => drinkCoffeeArea.SetActive(false);
}
