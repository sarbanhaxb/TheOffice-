using TMPro;
using UnityEngine;

public class CoolerInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    public GameObject drinkWaterArea;
    [SerializeField] private float interactionPriority = 5f;
    [SerializeField] private AudioSource pourWater;

    private void Awake()
    {
        pourWater = GetComponent<AudioSource>();
    }
    private void Start()
    {
        AudioManager.Instance.RegisterAudioSource(pourWater);
    }

    public void Interact()
    {
        if (PlayerCurrentState.Instance.GetCurrentState() != PlayerStates.DrinkingWater)
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.DrinkingWater);
            pourWater.Play();
        }
        else
        {
            PlayerCurrentState.Instance.SetState(PlayerStates.Idle);
            pourWater.Stop();
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
