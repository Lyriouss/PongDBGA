using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    
    [SerializeField] private float speed;

    private Rigidbody2D rb2DP1;
    private Rigidbody2D rb2DP2;

    private void Awake()
    {
        //Gets the Rigidbody2D of both Players
        rb2DP1 = player1.GetComponent<Rigidbody2D>();
        rb2DP2 = player2.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Calls bool function from InputManager, if there is a movement input from Player 1 then returns movement direction value
        if (InputManager.isMovingP1(out Vector2 p1Dir))
            //Passes the Rigidbody2D and movement direction to move Player 1
            MovePlayer(rb2DP1, p1Dir);
        else
            rb2DP1.linearVelocity = Vector2.zero;       //If Player 1 isn't moving then stops movement

        //Same as above but with Player 2 inputs
        if (InputManager.isMovingP2(out Vector2 p2Dir))
            MovePlayer(rb2DP2, p2Dir);
        else 
            rb2DP2.linearVelocity = Vector2.zero;
    }

    private void MovePlayer(Rigidbody2D rb2D, Vector2 dir)
    {
        //Moves the Rigidbody2D in a specified direction based on parameters passed
        rb2D.MovePosition(rb2D.position + (dir * (speed * Time.fixedDeltaTime)));
        //No clamp needed to keep players in camera view as the borders stop movement
    }
}
