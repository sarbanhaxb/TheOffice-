using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NPC_AIScript : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    float lastY = 0f;
    bool lastFlip = true;

    private const string MOVE_Y = "MoveY";
    private const string MOVE_X = "MoveX";

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AnimDirectionController();
    }






    //АНИМАЦИИ
    void AnimDirectionController()
    {
        Vector3 direction = _navMeshAgent.velocity.normalized;

        // Упрощение работы с анимацией
        float newY = direction.y != 0 ? direction.y : lastY;
        _animator.SetFloat(MOVE_Y, newY);
        lastY = newY;

        // Упрощение логики отражения спрайта
        bool shouldFlip = direction.x != 0 ? direction.x > 0 : lastFlip;
        _spriteRenderer.flipX = shouldFlip;
        lastFlip = shouldFlip;
    }

}
