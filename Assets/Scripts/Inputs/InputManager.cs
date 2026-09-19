using System;
using UnityEngine;

public class InputManager
{
    private static InputActions inputs;

    public static event Action OnStartGameRequested, OnScoreIncrement, OnScoreLower, OnPauseRequested,
        OnResumeRequested, OnReloadSceneRequested;
    
    public static Action OnStart, OnStartGameAllowed, OnPauseAllowed, OnResumeAllowed, OnGameWin;
    
    static InputManager()
    {
        //Creates an instance of InputActions
        inputs = new InputActions();
        //and enables it
        inputs.Enable();
        
        //Delegate actions that enables only a specific input map
        OnStart += SwitchToStartGame;
        OnStartGameAllowed += SwitchToPlay;
        OnPauseAllowed += SwitchToPause;
        OnResumeAllowed += SwitchToPlay;
        OnGameWin += SwitchToWinGame;

        //Triggers event action when key is pressed for StartGame input map actions
        inputs.StartGame.StartGame.performed += _ => OnStartGameRequested?.Invoke();
        inputs.StartGame.IncrementScore.performed += _ => OnScoreIncrement?.Invoke();
        inputs.StartGame.LowerScore.performed += _ => OnScoreLower?.Invoke();
        
        //Event action triggers for pause/resume of game
        inputs.Play.Pause.performed += _ => OnPauseRequested?.Invoke();
        inputs.Pause.Resume.performed += _ => OnResumeRequested?.Invoke();

        //Event action triggers for reloading the scene
        inputs.Play.ReloadScene.performed += _ => OnReloadSceneRequested?.Invoke();
        inputs.Pause.ReloadScene.performed += _ => OnReloadSceneRequested?.Invoke();
        inputs.WinGame.ReloadScene.performed += _ => OnReloadSceneRequested?.Invoke();
    }
    
    //Gets movement input value from P1Movement in InputActions class
    public static Vector2 GetPlayer1Movement => inputs.Play.P1Movement.ReadValue<Vector2>();
    
    //Same as above but with P2Movement
    public static Vector2 GetPlayer2Movement => inputs.Play.P2Movement.ReadValue<Vector2>();

    //Bool function that returns direction value of Player 1 input
    public static bool isMovingP1(out Vector2 dir)
    {
        //Gets movement value of Player 1 
        dir = new Vector2(0f, GetPlayer1Movement.y);
        //Returns movement value and if the value is not equal to zero then this function all returns true
        return dir != Vector2.zero;
    }

    //Same as above but for Player 2
    public static bool isMovingP2(out Vector2 dir)
    {
        dir = new Vector2(0f, GetPlayer2Movement.y);
        return dir != Vector2.zero;
    }

    //Deactivates all inputs and enables StartGame input map to be the only one active
    static void SwitchToStartGame()
    {
        inputs.Play.Disable();
        inputs.Pause.Disable();
        inputs.WinGame.Disable();
        inputs.StartGame.Enable();
    }

    //Deactivates all inputs and enables Play input map to be the only one active
    static void SwitchToPlay()
    {
        inputs.StartGame.Disable();
        inputs.Pause.Disable();
        inputs.WinGame.Disable();
        inputs.Play.Enable();
    }
    
    //Deactivates all inputs and enables Pause input map to be the only one active
    static void SwitchToPause()
    {
        inputs.StartGame.Disable();
        inputs.Play.Disable();
        inputs.WinGame.Disable();
        inputs.Pause.Enable();
    }

    //Deactivates all inputs and enables WinGame input map to be the only one active
    static void SwitchToWinGame()
    {
        inputs.StartGame.Disable();
        inputs.Play.Disable();
        inputs.Pause.Disable();
        inputs.WinGame.Enable();
    }
}
