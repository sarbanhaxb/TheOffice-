using System;
using UnityEngine;

public class PlayerCurrentState : MonoBehaviour
{
    public static PlayerCurrentState Instance {  get; private set; }

    public event Action<PlayerStates> OnStateChanged;
    [SerializeField] private PlayerStates currentState;

    private void Awake()
    {
        currentState = PlayerStates.Idle;
        Instance = this;
    }

    public void SetState(PlayerStates newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }
    public PlayerStates GetCurrentState() => currentState;
}

public enum PlayerStates
{
    Idle,
    Moving,
    Smoking,
    Talking,
    Present,
    Working,
    DrinkingWater,
    DrinkingCoffee,
    Microwaving,
    Tired,
    Eating
}