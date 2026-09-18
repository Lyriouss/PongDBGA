using UnityEngine;

public class InputManager
{
    private static InputActions inputs;
    
    static InputManager()
    {
        //Creates an instance of InputActions
        inputs = new InputActions();
        //and enables it
        inputs.Enable();
    }
    
    //Gets movement input value from P1Movement in InputActions class
    public static Vector2 GetPlayer1Movement => inputs.Player.P1Movement.ReadValue<Vector2>();
    
    //Same as above but with P2Movement
    public static Vector2 GetPlayer2Movement => inputs.Player.P2Movement.ReadValue<Vector2>();

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
}
