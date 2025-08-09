using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NPC_VisualScript : MonoBehaviour, EntityVisual
{
    [Header("UI элементы")]
    [SerializeField] private Canvas UIelement;
    [SerializeField] private Image stressBar;
    [SerializeField] private Image starveBar;
    [SerializeField] private Image thirstBar;
    [SerializeField] private Button salaryUp;
    [SerializeField] private Button salaryDown;
    [SerializeField] private TMP_Text currentSalary;

    private Animator _animator;
    private NPC_CurrentState _currentState;
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;
    private NPC_Stats _npcStats;
    private Color _originalColor;

    float lastY = 0f;
    bool lastFlip = true;

    private const string MOVE_Y = "MoveY";

    private void Awake()
    {
        _npcStats = GetComponent<NPC_Stats>();
        _animator = GetComponent<Animator>();
        _currentState = GetComponent<NPC_CurrentState>();
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        salaryUp.onClick.AddListener(_npcStats.IncreaseSalary);
        salaryDown.onClick.AddListener(_npcStats.DecreaseSalary);
    }

    private void Update()
    {
        UpdateNPCDirection();
        UpdateStatsScale();
    }

    private void UpdateStatsScale()
    {
        stressBar.fillAmount = _npcStats.GetStressRatio();
        starveBar.fillAmount = _npcStats.GetStarveRatio();
        thirstBar.fillAmount = _npcStats.GetThirstRatio();
        currentSalary.text = _npcStats.GetCurrentSalary().ToString() + "$";
    }



    private void UpdateNPCDirection()
    {
        if (!_animator.GetBool("IsWorking") && !_animator.GetBool("IsMeeting"))
        {
            Vector2 direction = _agent.velocity.normalized;

            // ”прощение работы с анимацией
            float newY = direction.y != 0 ? direction.y : lastY;
            _animator.SetFloat(MOVE_Y, newY);
            lastY = newY;

            // ”прощение логики отражени€ спрайта
            bool shouldFlip = direction.x != 0 ? direction.x > 0 : lastFlip;
            _spriteRenderer.flipX = shouldFlip;
            lastFlip = shouldFlip;
        }
        else if (_animator.GetBool("IsMeeting"))
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void ResetAnimator()
    {
        _animator.Rebind();
    }

    public void TurnOffAnimatorBoolean(string animation)
    {
        _animator.SetBool(animation, false);
    }


    public void ShowBarAnimation()
    {
        UIelement.enabled = true;
        _spriteRenderer.color = Color.green;
    }

    public void HideBarAnimation()
    {
        UIelement.enabled = false;
        _spriteRenderer.color = _originalColor;
    }
    public void SetRandom(int count) => _animator.SetFloat("Random", Random.Range(0, count));

}