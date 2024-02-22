using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private int score;
    private int scoreForPreviousSession;
    public bool isGameActive;
    public bool isGameOver;

    private int totalScore;
    private string userName;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] GameObject pausedScreen;
    [SerializeField] Button mainMenuButton;
    [SerializeField] GameObject menuManager;
    [SerializeField] GameObject gameOverScreen;

    private void Awake()
    {
        scoreForPreviousSession = MainManager.Instance.GetTotalScore();
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        isGameOver = false;
        scoreText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGameActive = false;
                PauseGame();
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isGameActive = true;
                ResumeGame();
            }

        }
    }

    //���������� ����� � ������� ������
    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }

    }

    //�������� ����� ����� ������� �� ������ �������
    public void RestartGame() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //������� ��� Game Over
    public void GameOver(bool isGameOverTrigger)
    {
        if (isGameOverTrigger)
        {
            gameOverScreen.SetActive(true);
            //��������� ����� �� ��� ���������� ������� ������
            totalScore = scoreForPreviousSession + score;
            totalScoreText.text = "Total score: " + totalScore;
            totalScoreText.gameObject.SetActive(true);
            userName = MainManager.Instance.GetUserName();
            MainManager.Instance.SetTotalScore(totalScore);
            Debug.Log("Game over total score: " + totalScore);
            MainManager.Instance.Setleadrboard(userName, totalScore);

        }
    }

    public void PauseGame()
    {
        if (!isGameActive && !isGameOver)
        {
            Time.timeScale = 0f;
            // ��������� ������ ������
            pausedScreen.gameObject.SetActive(true);
            //��������� �������� ������ ����
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (isGameActive && !isGameOver)
        {
            Time.timeScale = 1f;
            pausedScreen.gameObject.SetActive(false);
            //��������� �������� ������ ����
            mainMenuButton.gameObject.SetActive(false);
        }
    }

    //����� �� ����
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

}
