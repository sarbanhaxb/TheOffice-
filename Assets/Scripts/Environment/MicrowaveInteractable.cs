using UnityEngine;
using UnityEngine.Audio;

public class MicrowaveInteractable : MonoBehaviour, IInteractable
{
    [Header("������")]
    [SerializeField] private GameObject microwaveArea;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector3 foodSpawnPoint;
    [SerializeField] private AudioSource microwaveSound;
    public float maxDistance = 10f;
    public float minVolume = 0.1f; // ����������� ��������� (�� 0, ����� ���� �� �������� ���������)

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

    private void Update()
    {
        if (microwaveSound.isPlaying)
        {
            // ��������� ���������� �� ������
            float distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            // ����������� ���������� �� 0 �� 1
            float normalizedDistance = Mathf.Clamp01(distance / maxDistance);

            // ����������� � ��������� � ��������� (��� ������, ��� ����)
            microwaveSound.volume = Mathf.Lerp(1f, minVolume, normalizedDistance);
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
