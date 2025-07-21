using UnityEngine;
using UnityEngine.InputSystem;

public class TapeRecorder : MonoBehaviour, IInteractable
{
    [Header("Настройки магнитофона")]
    [SerializeField] private float interactionPriority = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite turnedOffSprite;
    [SerializeField] private Sprite turnedOnSprite;
    [SerializeField] private AudioClip switchSound;

    [Header("Подсказка")]
    [SerializeField] private GameObject intecartionHing;

    [Header("Радио")]
    [SerializeField] private RadioSystem radioSystem;

    private bool _isTurnedOn = false;

    private void OnEnable()
    {
        GameInput.Instance.playerInputActions.Player.NextStation.Enable();
        GameInput.Instance.playerInputActions.Player.PrevStation.Enable();
    }

    private void OnDisable()
    {
        GameInput.Instance.playerInputActions.Player.NextStation.Disable();
        GameInput.Instance.playerInputActions.Player.PrevStation.Disable();
    }

    public float GetPriority() => interactionPriority;


    public void Interact()
    {
        AudioSource.PlayClipAtPoint(switchSound, transform.position);
        ToggleTapeRecorder();
    }

    public void ShowHint()
    {
        if (intecartionHing != null)
            intecartionHing.SetActive(true);
    }

    public void HideHint()
    {
        if (intecartionHing != null)
            intecartionHing.SetActive(false);
    }

    private void ToggleTapeRecorder()
    {
        _isTurnedOn = !_isTurnedOn;
        UpdateVisualState();

        if (_isTurnedOn)
        {
            radioSystem.TurnOn();
            GameInput.Instance.playerInputActions.Player.NextStation.performed += ctx => radioSystem.NextStation();
            GameInput.Instance.playerInputActions.Player.PrevStation.performed += ctx => radioSystem.PreviousStation();
        }
        else
        {
            radioSystem.TurnOff();
            GameInput.Instance.playerInputActions.Player.NextStation.performed -= ctx => radioSystem.NextStation();
            GameInput.Instance.playerInputActions.Player.PrevStation.performed -= ctx => radioSystem.PreviousStation();
        }

    }
    private void UpdateVisualState()
    {
        spriteRenderer.sprite = _isTurnedOn ? turnedOnSprite : turnedOffSprite;
    }
    private void OnValidate()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
