using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private int player1Score = 0;
    private int player2Score = 0;

    [Header("Ball")]
    [SerializeField] private GameObject ball;
    [SerializeField] private float maxBallSpawnHeight;
    [SerializeField] private float minBallSpawnHeight;
    [SerializeField] private float timeBeforeRespawn;
    
    public static event Action<Vector2> OnBallReset;
    public static event Action<int, int> OnUpdateScoreUI;
    
    private void OnEnable()
    {
        ScoreZone.OnPlayerScore += PlayerScore;
    }

    private void OnDisable()
    {
        ScoreZone.OnPlayerScore -= PlayerScore;
    }

    private void Start()
    {
        //Gives a 50/50 chance to have the ball either travel left or right at start
        bool randomXDirection = Random.value > 0.5f;
        //Resets the ball
        ResetBall(randomXDirection);
    }

    private void PlayerScore(ScorePointFor player)
    {
        //If Player 1 scored it will be true, else false
        bool spawnTowardsRight = player == ScorePointFor.Player1;
        
        //Based on the enum of passed through parameter, it will give a point to either Player 1 or 2
        if (player == ScorePointFor.Player1)
            player1Score++;
        else
            player2Score++;
        
        //Updates the score on UI through event action (UIManager)
        OnUpdateScoreUI?.Invoke(player1Score, player2Score);
        
        //Starts coroutine to delay the spawn of ball after scoring
        StartCoroutine(SpawnBallDelay());
        
        //Resets the ball
        ResetBall(spawnTowardsRight);
    }

    private void ResetBall(bool spawnTowardsRight)
    {
        //Resets the ball to a random point on y axis within the specified range
        float yPos = Random.Range(minBallSpawnHeight, maxBallSpawnHeight);
        ball.transform.position = new Vector2(0f, yPos);

        //Based on the bool in function parameter, if true the ball will right else it will go left
        float xDir = spawnTowardsRight ? 1f : -1f;
        //Gets a random direction angle to launch the ball
        float yDir = Random.Range(-0.5f, 0.5f);
        //Variable to store the new direction at which the ball will travel
        Vector2 newDir = new Vector2(xDir, yDir);
        
        //Resets ball stats and gives new direction to BallMovement class
        OnBallReset?.Invoke(newDir);
    }

    private IEnumerator SpawnBallDelay()
    {
        //Deactivates ball
        ball.SetActive(false);
        
        //Waits a set amount of time
        yield return new WaitForSeconds(timeBeforeRespawn);
        
        //Reactivates ball
        ball.SetActive(true);
    }
}
