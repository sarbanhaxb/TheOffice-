using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Customer_Visual : MonoBehaviour
{
    private Animator _animator;
    private NPC_CurrentState _currentState;
    private NavMeshAgent _agent;
    private SpriteRenderer _spriteRenderer;
    private NPC_Stats _npcStats;
    private NavMeshAgent _nma;
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
        _nma = GetComponent<NavMeshAgent>();
        _nma.updateRotation = false;
        _nma.updateUpAxis = false;

    }

    private void Update()
    {
        UpdateNPCDirection();
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
}

