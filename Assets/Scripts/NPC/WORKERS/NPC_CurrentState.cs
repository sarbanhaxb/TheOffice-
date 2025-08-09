using System;
using UnityEngine;

public class NPC_CurrentState : MonoBehaviour
{
    public event Action<NPCStates> OnStateChanged;
    public HandleStateChange handleStateChange;
    [SerializeField] private NPCStates currentState;

    private void Awake()
    {
        currentState = NPCStates.Empty;
        handleStateChange = new HandleStateChange();
    }

    public void SetState(NPCStates newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
        handleStateChange.SendEventMessage();
    }

    public NPCStates GetCurrentState() => currentState;

    public void ResetState()
    {
        currentState = NPCStates.Empty;
        handleStateChange.SendEventMessage();
    }
}
