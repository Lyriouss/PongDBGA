using System;
using UnityEngine;

public enum ScorePointFor
{
    Player1,
    Player2
}

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private ScorePointFor scorePointFor;
    
    public static event Action<ScorePointFor> OnPlayerScore;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the ball reaches a score zone
        if (collision.CompareTag("Ball"))
        {
            //Updates the score in GameManager based on zone
            OnPlayerScore?.Invoke(scorePointFor);
        }
    }
}
