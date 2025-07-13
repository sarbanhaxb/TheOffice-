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

    private Animator _animator;
    private NPC_CurrentState _currentState;
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;
    private NPCStats _npcStats;
    private Color _originalColor;

    float lastY = 0f;
    bool lastFlip = true;

    private const string MOVE_Y = "MoveY";

    private void Awake()
    {
        _npcStats = GetComponent<NPCStats>();
        _animator = GetComponent<Animator>();
        _currentState = GetComponent<NPC_CurrentState>();
        _agent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
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
    }



    private void UpdateNPCDirection()
    {
        if (!_animator.GetBool("IsWorking"))
        {
            Vector2 direction = _agent.velocity.normalized;

            // Упрощение работы с анимацией
            float newY = direction.y != 0 ? direction.y : lastY;
            _animator.SetFloat(MOVE_Y, newY);
            lastY = newY;

            // Упрощение логики отражения спрайта
            bool shouldFlip = direction.x != 0 ? direction.x > 0 : lastFlip;
            _spriteRenderer.flipX = shouldFlip;
            lastFlip = shouldFlip;
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


    public void ShowBarAnimation()
    {
        UIelement.enabled = true;
        _spriteRenderer.color = Color.green;
        Debug.Log($"В меня ({gameObject.name}) тыкнули");
    }

    public void HideBarAnimation()
    {
        UIelement.enabled = false;
        _spriteRenderer.color = _originalColor;
        Debug.Log($"В не меня ({gameObject.name}) тыкнули");

    }
    public void SetRandom(int count) => _animator.SetFloat("Random", Random.Range(0, count));

}