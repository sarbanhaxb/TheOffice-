using System;
using UnityEngine;

public class NPC_CurrentState : MonoBehaviour
{
    public event Action<NPCStates> OnStateChanged;
    [SerializeField] private NPCStates currentState;

    private void Awake()
    {
        currentState = NPCStates.GoWorking;
    }

    public void SetState(NPCStates newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public NPCStates GetCurrentState() => currentState;
}
