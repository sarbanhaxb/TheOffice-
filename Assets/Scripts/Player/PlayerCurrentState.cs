using System;
using UnityEngine;

public class PlayerCurrentState : MonoBehaviour
{
    public static PlayerCurrentState Instance {  get; private set; }

    public event Action<PlayerStates> OnStateChanged;
    [SerializeField] private PlayerStates currentState;

    static int x = 1;

    private void Awake()
    {
        currentState = PlayerStates.Idle;
        Instance = this;
    }

    public void SetState(PlayerStates newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(newState);
        x++;
        Debug.Log($"Метод вызван {x} раз.");
    }

    private void Update()
    {
        Debug.Log(currentState);
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