using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Start Game Panel")] 
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private TMP_Text scoreToWinNum;
    
    [Header("Scoreboard Panel")]
    [SerializeField] private GameObject scoreboardPanel;
    [SerializeField] private TMP_Text p1ScoreTxt;
    [SerializeField] private TMP_Text p2ScoreTxt;
    
    [Header("Pause Game Panel")]
    [SerializeField] private GameObject pauseGamePanel;
    
    [Header("Game Win Panel")]
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private TMP_Text gameWinTxt;
    
    private void OnEnable()
    {
        GameManager.OnStartGame += UpdateGameplayUI;
        
        GameManager.OnUpdateScoreUI += UpdateScore;
        GameManager.OnUpdateScoreToWinUI += UpdateScoreToWin;
        GameManager.OnPauseGame += OpenPauseGamePanel;
        GameManager.OnResumeGame += ClosePauseGamePanel;

        GameManager.OnWinGame += OpenWinGamePanel;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= UpdateGameplayUI;
        
        GameManager.OnUpdateScoreUI -= UpdateScore;
        GameManager.OnUpdateScoreToWinUI -= UpdateScoreToWin;
        GameManager.OnPauseGame -= OpenPauseGamePanel;
        GameManager.OnResumeGame -= ClosePauseGamePanel;

        GameManager.OnWinGame -= OpenWinGamePanel;
    }

    private void Start()
    {
        //Only activates the panel to be shown on scene start
        startGamePanel.SetActive(true);
        scoreboardPanel.SetActive(false);
        pauseGamePanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    //Updates the text of score to win in GameStartPanel
    private void UpdateScoreToWin(int score) => scoreToWinNum.text = score.ToString();

    private void UpdateGameplayUI()
    {
        //Deactivates startGamePanel to then activate the necessary panel for gameplay
        startGamePanel.SetActive(false);
        scoreboardPanel.SetActive(true);
    }

    private void UpdateScore(int p1Score, int p2Score)
    {
        //Updates the text of scoreboard to parameter values in UI
        p1ScoreTxt.text = p1Score.ToString();
        p2ScoreTxt.text = p2Score.ToString();
    }
    
    private void OpenPauseGamePanel() => pauseGamePanel.SetActive(true);        //Activates the PauseGamePanel
    private void ClosePauseGamePanel() => pauseGamePanel.SetActive(false);      //Deactivates the PauseGamePanel

    private void OpenWinGamePanel(string player)
    {
        gameWinPanel.SetActive(true);               //Activates the gameWinPanel
        gameWinTxt.text = player + " wins!!";       //Then updates the text to show which player won
    }
}
