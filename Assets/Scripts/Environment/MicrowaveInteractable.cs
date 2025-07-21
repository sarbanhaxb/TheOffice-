using UnityEngine;

public class MicrowaveInteractable : MonoBehaviour, IInteractable
{
    [Header("—сылки")]
    [SerializeField] private GameObject microwaveArea;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector3 foodSpawnPoint;
    [SerializeField] private AudioSource microwaveSound;

    [SerializeField] private float interactionPriority = 5f;


    private Animator _animator;
    private const string IS_MAKE_FOOD = "IsMakeFood";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        microwaveSound = GetComponent<AudioSource>();
        AudioManager.Instance.RegisterAudioSource(microwaveSound);
    }

    public void Interact()
    {
        if (!_animator.GetBool(IS_MAKE_FOOD))
        {
            _animator.SetBool(IS_MAKE_FOOD, true);
            microwaveSound.Play();
        }
    }
    public void AddFood() => Instantiate(foodPrefab, foodSpawnPoint, Quaternion.identity);
    public void ShowHint()
    {
        microwaveArea.SetActive(true);
    }
    public void HideHint()
    {
        microwaveArea.SetActive(false);
    }

    public void ResetAnim() => _animator.SetBool(IS_MAKE_FOOD, false);
    public float GetPriority() => interactionPriority;
}
