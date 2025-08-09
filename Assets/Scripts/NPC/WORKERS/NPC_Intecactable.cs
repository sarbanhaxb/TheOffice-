using Unity.Behavior;
using UnityEngine;

public class NPC_Intecactable : MonoBehaviour, IInteractable
{

    private NPC_CurrentState NPC_CurrentState;
    [SerializeField] private float interactionPriority = 1f;

    private void Awake()
    {
        NPC_CurrentState = GetComponent<NPC_CurrentState>();
    }

    public float GetPriority() => interactionPriority;


    public void HideHint()
    {
    }

    public void Interact()
    {
        NPC_CurrentState.SetState(NPCStates.Speak);
    }

    public void ShowHint()
    {
    }
}
