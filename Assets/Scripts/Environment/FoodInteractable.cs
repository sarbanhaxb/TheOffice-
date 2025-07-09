using UnityEngine;

public class FoodInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    [SerializeField] private GameObject eatingCoffeeArea;
    [SerializeField] private float interactionPriority = 10f;

    public float GetPriority() => interactionPriority;
    public void Interact()
    {
        PlayerCurrentState.Instance.SetState(PlayerStates.Eating);
        Destroy(gameObject);
    }
    public void ShowHint() => eatingCoffeeArea.SetActive(true);
    public void HideHint() => eatingCoffeeArea.SetActive(false);
}
