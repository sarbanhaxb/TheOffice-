using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class LightOnInteractable : MonoBehaviour, IInteractable
{

    [Header("Настройки магнитофона")]
    [SerializeField] private float interactionPriority = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite turnedOffSprite;
    [SerializeField] private Sprite turnedOnSprite;
    [SerializeField] private AudioSource switchSound;
    [SerializeField] private GameObject intecartionHing;
    [SerializeField] private Light2D light2D;
    private bool switcher = false;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        switchSound = GetComponent<AudioSource>();
        light2D = GetComponentInChildren<Light2D>();
        light2D.enabled = false;
    }

    public float GetPriority() => interactionPriority;


    public void Interact()
    {
        if (!switcher)
        {
            light2D.enabled = true;
            switchSound.Play();
            switcher = !switcher;
        }
        else
        {
            light2D.enabled= false;
            switchSound.Play();
            switcher = !switcher;
        }
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
}
