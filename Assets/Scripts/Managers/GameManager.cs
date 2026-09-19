using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private int player1Score;
    private int player2Score;

    private int scoreToWin;
    [SerializeField] private int lowestScoreToWin;
    [SerializeField] private int highestScoreToWin;

    [Header("Players")] 
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Bounds")] 
    [SerializeField] private Collider2D borderLeft;
    [SerializeField] private Collider2D borderRight;

    [Header("Ball")]
    [SerializeField] private GameObject ball;
    [SerializeField] private float maxBallSpawnHeight;
    [SerializeField] private float minBallSpawnHeight;
    [SerializeField] private float timeBeforeRespawn;
    
    public static event Action<Vector2> OnBallReset;
    public static event Action<int, int> OnUpdateScoreUI;
    public static event Action<int> OnUpdateScoreToWinUI;
    public static event Action OnStartGame, OnPauseGame, OnResumeGame;
    public static event Action<string> OnWinGame;
    
    private void OnEnable()
    {
        InputManager.OnStartGameRequested += StartGame;
        InputManager.OnScoreIncrement += IncrementScoreToWin;
        InputManager.OnScoreLower += LowerScoreToWin;
        
        InputManager.OnPauseRequested += PauseGame;
        InputManager.OnResumeRequested += ResumeGame;

        InputManager.OnReloadSceneRequested += ReloadScene;
        
        ScoreZone.OnPlayerScore += PlayerScore;
    }

    private void OnDisable()
    {
        InputManager.OnStartGameRequested -= StartGame;
        InputManager.OnScoreIncrement -= IncrementScoreToWin;
        InputManager.OnScoreLower -= LowerScoreToWin;
        
        InputManager.OnPauseRequested -= PauseGame;
        InputManager.OnResumeRequested -= ResumeGame;

        InputManager.OnReloadSceneRequested -= ReloadScene;
        
        ScoreZone.OnPlayerScore -= PlayerScore;
    }

    private void Start()
    {
        //Always sets timeScale to 1 and correct input maps active at scene start
        Time.timeScale = 1f;
        InputManager.OnStart?.Invoke();
        
        //Sets player scores to 0
        player1Score = 0;
        player2Score = 0;
        //And updates the UI to match
        OnUpdateScoreUI?.Invoke(player1Score, player2Score);
       
        //Deactivates player paddles
        player1.SetActive(false);
        player2.SetActive(false);
        //And sets the left and right border to not be triggers so the ball can bounce around in background
        borderLeft.isTrigger = false;
        borderRight.isTrigger = false;
        
        LoadScoreToWin();
        
        ShootBallInRandomDirection();
    }

    private void LoadScoreToWin()
    {
        //Loads the PlayerPrefs value of score to win with a default of 9 if there is no value saved at said key
        scoreToWin = PlayerPrefs.GetInt("ScoreToWin", 9);
        //Updates the score to win in UI Manager
        OnUpdateScoreToWinUI?.Invoke(scoreToWin);
    }

    private void IncrementScoreToWin()
    {
        //Increments the scoreToWin value
        scoreToWin++;
        //If the scoreToWin value goes over max value, then clamps it to highest value
        if (scoreToWin > highestScoreToWin)
            scoreToWin = highestScoreToWin;
        
        //Updates the UI with new scoreToWin value
        OnUpdateScoreToWinUI?.Invoke(scoreToWin);
    }

    private void LowerScoreToWin()
    {
        //Lowers the scoreToWin value
        scoreToWin--;
        //If the scoreToWin value goes under the min value, then clamps it to lowest value
        if (scoreToWin < lowestScoreToWin)
            scoreToWin = lowestScoreToWin;
        
        //Updates the UI with new scoreToWin value
        OnUpdateScoreToWinUI?.Invoke(scoreToWin);
    }

    private void StartGame()
    {
        //Saves the scoreToWin value in PlayerPrefs
        PlayerPrefs.SetInt("ScoreToWin", scoreToWin);
        
        //Activates Player paddles
        player1.SetActive(true);
        player2.SetActive(true);
        //Changes left and right borders to be triggers
        borderLeft.isTrigger = true;
        borderRight.isTrigger = true;
        
        //Reset the ball at center and shoots it in a random direction
        ShootBallInRandomDirection();
        
        //Updates all necessary factors in UI and Input Managers
        OnStartGame?.Invoke();
        InputManager.OnStartGameAllowed?.Invoke();
    }

    private void PauseGame()
    {
        //Pauses game and changes all necessary factors in UI and Input Managers for pause
        Time.timeScale = 0f;
        OnPauseGame?.Invoke();
        InputManager.OnPauseAllowed?.Invoke();
    }

    private void ResumeGame()
    {
        //Resumes game and changes all necessary factors in UI and Input Managers for resume
        Time.timeScale = 1f;
        OnResumeGame?.Invoke();
        InputManager.OnResumeAllowed?.Invoke();
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
        
        //Makes a check for win condition
        CheckForWinCondition(player1Score, player2Score);
        
        //Resets the ball
        ResetBall(spawnTowardsRight);
        
        //Starts coroutine to delay the spawn of ball after scoring
        StartCoroutine(SpawnBallDelay());
    }

    private void CheckForWinCondition(int player1Score, int player2Score)
    {
        //Checks if Player 1 reached score threshold
        if (player1Score >= scoreToWin)
        {
            //If so then pauses game and updates all necessary factors in UI and Input Managers for game win of Player 1
            Time.timeScale = 0f;
            OnWinGame?.Invoke("Player 1");
            InputManager.OnGameWin?.Invoke();
        }
        //Checks if Player 2 reached score threshold instead
        else if (player2Score >= scoreToWin)
        {
            //If so then pauses game and updates all necessary factors in UI and Input Managers for game win of Player 2
            Time.timeScale = 0f;
            OnWinGame?.Invoke("Player 2");
            InputManager.OnGameWin?.Invoke();
        }
    }

    private void ShootBallInRandomDirection()
    {
        //Gives a 50/50 chance to have the ball either travel left or right at start
        bool randomXDirection = Random.value > 0.5f;
        //Resets the ball
        ResetBall(randomXDirection);
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

    private void ReloadScene()
    {
        //Reloads the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
