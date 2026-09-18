using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float topmostSpawnHeight;
    [SerializeField] private float bottommostSpawnHeight;
    
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
        rb2D.MovePosition(rb2D.position + (direction * speed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision);
    }

    private void Bounce(Collision2D collision)
    {
        //Gets the normal of the contact point where the ball hit
        Vector2 colNormal = collision.GetContact(0).normal;
        //Uses Reflect() method to change the direction of ball movement to simulate a bounce
        direction = Vector2.Reflect(direction, colNormal).normalized;
    }
}
