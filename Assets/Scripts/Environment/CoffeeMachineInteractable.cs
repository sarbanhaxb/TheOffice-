using UnityEngine;

public class CoffeeMachineInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    [SerializeField] private GameObject coffeeArea;
    [SerializeField] private GameObject coffeePrefab;
    [SerializeField] private Vector3 coffeeSpawnPoint;

    [SerializeField] private float interactionPriority = 5f;


    private Animator _animator;
    private const string IS_MAKE_COFFEE = "IsMakeCoffee";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        _animator.SetBool(IS_MAKE_COFFEE, true);
    }
    public void AddCoffeeMug() => Instantiate(coffeePrefab, coffeeSpawnPoint, Quaternion.identity);
    public void ShowHint()
    {
        coffeeArea.SetActive(true);
    }
    public void HideHint()
    {
        coffeeArea.SetActive(false);
    }

    public void ResetAnim() => _animator.SetBool(IS_MAKE_COFFEE, false);
    public float GetPriority() => interactionPriority;
}