using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChange;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        UpdateGameState(GameState.GameRunning); 
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch(newState)
        {
            case GameState.GamePaused:
                break;
            case GameState.GameRunning:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null); 
        }
        OnGameStateChange?.Invoke(newState); 
    }    
}


public enum GameState
{
    GamePaused,
    GameRunning
}