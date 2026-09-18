using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text p1ScoreTxt;
    [SerializeField] private TMP_Text p2ScoreTxt;
    
    private void OnEnable()
    {
        GameManager.OnUpdateScoreUI += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateScoreUI -= UpdateScore;
    }

    private void UpdateScore(int p1Score, int p2Score)
    {
        //Updates the text to parameter values in UI
        p1ScoreTxt.text = p1Score.ToString();
        p2ScoreTxt.text = p2Score.ToString();
    }
}
