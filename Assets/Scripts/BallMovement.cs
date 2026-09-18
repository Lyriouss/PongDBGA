using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float incrementSpeed;
    [SerializeField] private float maxSpeed;

    private Rigidbody2D rb2D;
    private Vector2 direction;
    private float speed;

    private void Awake()
    {
        //Gets the Rigibody2D of the script object
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameManager.OnBallReset += ResetBall;
    }

    private void OnDisable()
    {
        GameManager.OnBallReset -= ResetBall;
    }

    private void ResetBall(Vector2 newDir)
    {
        //Sets ball speed to start speed and gives new move direction based on parameter value
        speed = startSpeed;
        direction = newDir;
    }

    private void FixedUpdate()
    {
        //Moves the ball at a constant speed using rigidbody physics
        rb2D.MovePosition(rb2D.position + (direction * (speed * Time.fixedDeltaTime)));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If the collision is with an object with tag "Player"
        if (collision.gameObject.CompareTag("Player"))
            //Makes a specific calculation of bounce based on point of collision with Player
            BounceOnPlayer(collision);
        else
            //Makes a calculation of bounce with normal of border
            BounceOnBorder(collision);
    }

    private void BounceOnPlayer(Collision2D collision)
    {
        //Gets the transform of player collided with
        Transform playerPos = collision.transform;
        
        //Calculates the normalized direction from the player to the ball
        Vector2 dirToPlayer = (transform.position - playerPos.position).normalized;
        
        //Clamps the direction of the y axis so the bounce angle doesn't exceed 45 degrees based on the normal
        float yDir = Mathf.Clamp(dirToPlayer.y, -0.5f, 0.5f);
        //Gets the direction in which the ball is supposed to bounce horizontally (left or right)
        float xDir = (transform.position.x > playerPos.position.x) ? 1f : -1f;
        
        //Sets a new direction to the ball based on the above variables
        direction = new Vector2(xDir, yDir);
        
        //Increments the speed after each hit by player without going over max value
        speed += incrementSpeed;
        if (speed > maxSpeed)
            speed = maxSpeed;
    }

    private void BounceOnBorder(Collision2D collision)
    {
        //Gets the normal of the contact point where the ball hit
        Vector2 colNormal = collision.GetContact(0).normal;
        //Uses Reflect() method to change the direction of ball movement to simulate a bounce
        direction = Vector2.Reflect(direction, colNormal).normalized;
    }
}
