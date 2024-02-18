using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] int indexBronzeRankOne = 0;
    [SerializeField] int indexBronzeRankTwo = 1;
    [SerializeField] int indexBronzeRankThree = 2;
    //private int indexSilverRankOne = 3;
    [SerializeField] int score;
    [SerializeField] int scoreForPreviousSession;
    [SerializeField] int totalScoreForAllSession;
    [SerializeField] int maxScoreBeforeRankBronzeTwo;
    [SerializeField] int maxScoreBeforeRankBronzeThree;
    [SerializeField] int maxScoreBeforeRankBronzeOne;
    public bool isGameActive;
    public bool isGameOver;
    private int totalScore;
    private string userName;

    [SerializeField] GameObject sliderProgressRankBar;
    [SerializeField] List<Image> rankImages;
    [SerializeField] List<Image> currentRank;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] GameObject menuManager;


    private void Awake()
    {
        scoreForPreviousSession = MainManager.Instance.GetTotalScore();
        maxScoreBeforeRankBronzeTwo = 200000;
        maxScoreBeforeRankBronzeThree = 6000000;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        isGameOver = false;
        CurrentRank();
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
            //��������� ������ Game Over'a
            gameOverText.gameObject.SetActive(true);
            //��������� ������ �����������
            restartButton.gameObject.SetActive(true);
            //���������� �������� ���������
            sliderProgressRankBar.SetActive(true);
            //��������� ������ ������
            exitButton.gameObject.SetActive(true);
            //��������� �������� ������ ����
            mainMenuButton.gameObject.SetActive(true);
            isGameOver = true;
            //���������� �������� ���������
            sliderProgressRankBar.SetActive(true);
            //��������� ����� �� ��� ���������� ������� ������
            totalScore = scoreForPreviousSession + score;
            totalScoreText.text = "Total score: " + totalScore;
            totalScoreText.gameObject.SetActive(true);
            userName = MainManager.Instance.GetUserName();
            MainManager.Instance.SetTotalScore(totalScore);
            Debug.Log("Game over total score: " + totalScore);
            MainManager.Instance.Setleadrboard(userName, totalScore);
            //��������� ����� �� ��� ������� ������
            //����������� �������� ����������
            RankPorgression();
        }
    }

    //����������� ������ ������ �� ���������� �����
    public void CurrentRank() //ABSTRACTION 
    {
        totalScoreForAllSession = scoreForPreviousSession;
        //����������� ����� ������ 3
        if (totalScoreForAllSession < maxScoreBeforeRankBronzeTwo)
        {
            currentRank[indexBronzeRankThree].gameObject.SetActive(true);
        }
        else
        {
            currentRank[indexBronzeRankTwo].gameObject.SetActive(true);
        }
    }


    public void PauseGame()
    {
        if (!isGameActive && !isGameOver)
        {
            Time.timeScale = 0f;
            // ��������� ������ ������
            exitButton.gameObject.SetActive(true);
            //��������� �������� ������ ����
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (isGameActive && !isGameOver)
        {
            Time.timeScale = 1f;
            exitButton.gameObject.SetActive(false);
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
    }

    //����������� ���������� ������
    private void RankPorgression()
    {
        //����������� ��������� ������ ������ 3 -> 2
        if (totalScore < maxScoreBeforeRankBronzeTwo)
        {
            //���������� ����� �������� ��� �������� ���������
            sliderProgressRankBar.GetComponent<Slider>().value = totalScore;
            rankImages[indexBronzeRankThree].gameObject.SetActive(true);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
        }
        //����������� ��������� ������ ������ 2 -> 1
        if (totalScore > maxScoreBeforeRankBronzeTwo)
        {
            //���������� ����� �������� ��� �������� ���������
            sliderProgressRankBar.GetComponent<Slider>().minValue = maxScoreBeforeRankBronzeTwo;
            sliderProgressRankBar.GetComponent<Slider>().maxValue = maxScoreBeforeRankBronzeThree;
            sliderProgressRankBar.GetComponent<Slider>().value = totalScore;
            //����������� ����������� ����� ������ 2 �� ������� ������ 3
            rankImages[indexBronzeRankTwo].gameObject.transform.position = new Vector3(rankImages[indexBronzeRankThree].transform.position.x, rankImages[indexBronzeRankThree].transform.position.y);
            //��������� ����� ������ � ����������� ����� ������ 
            rankImages[indexBronzeRankThree].gameObject.SetActive(false);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
            rankImages[indexBronzeRankOne].gameObject.SetActive(true);

        }
    }



}
