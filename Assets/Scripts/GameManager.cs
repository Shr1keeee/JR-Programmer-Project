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
    public bool isGameActive;
    public bool isGameOver;
    [SerializeField] int scoreForPreviousSession;
    [SerializeField] int totalScoreForAllSession;
    [SerializeField] int maxScoreBeforeRankBronzeTwo;
    [SerializeField] int maxScoreBeforeRankBronzeThree;

    [SerializeField] GameObject sliderProgressRankBar;
    [SerializeField] List<Image> rankImages;
    [SerializeField] List<Image> currentRank;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button mainMenuButton;




    private void Awake()
    {
        //���������� Max Value � Min Value � Progress Rank Slider
        //LoadTotalScoreForAllSession();
        maxScoreBeforeRankBronzeTwo = 200000;
        maxScoreBeforeRankBronzeThree = 600000;
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        isGameOver = false;
        CurrentRank();
        scoreText.gameObject.SetActive(true);
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

            mainMenuButton.gameObject.SetActive(true);

            isGameActive = false;
            isGameOver = true;
            //���������� �������� ���������
            sliderProgressRankBar.SetActive(true);
            //��������� ����� �� ��� ���������� ������� ������
            scoreForPreviousSession = MainManager.Instance.LoadTotalScoreForAllSession();
            //���������� ����� �� ������� ������ � ����� ����� �� ���� ���������� 
            MainManager.Instance.SaveTotalScoreForAllSession(scoreForPreviousSession, score);
            //��������� ����� �� ��� ������� ������
            totalScoreForAllSession = MainManager.Instance.LoadTotalScoreForAllSession();
            //����������� �������� ����������
            RankPorgression();

        }
    }

    //����������� ������ ������ �� ���������� �����
    public void CurrentRank()
    {
        totalScoreForAllSession = MainManager.Instance.LoadTotalScoreForAllSession();
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
        if (totalScoreForAllSession < maxScoreBeforeRankBronzeTwo)
        {
            //���������� ����� �������� ��� �������� ���������
            sliderProgressRankBar.GetComponent<Slider>().value = totalScoreForAllSession;
            rankImages[indexBronzeRankThree].gameObject.SetActive(true);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
        }
        //����������� ��������� ������ ������ 2 -> 1
        if (totalScoreForAllSession > maxScoreBeforeRankBronzeTwo)
        {
            //���������� ����� �������� ��� �������� ���������
            sliderProgressRankBar.GetComponent<Slider>().minValue = maxScoreBeforeRankBronzeTwo;
            sliderProgressRankBar.GetComponent<Slider>().maxValue = maxScoreBeforeRankBronzeThree;
            sliderProgressRankBar.GetComponent<Slider>().value = totalScoreForAllSession;
            //����������� ����������� ����� ������ 2 �� ������� ������ 3
            rankImages[indexBronzeRankTwo].gameObject.transform.position = new Vector3(rankImages[indexBronzeRankThree].transform.position.x, rankImages[indexBronzeRankThree].transform.position.y);
            //��������� ����� ������ � ����������� ����� ������ 
            rankImages[indexBronzeRankThree].gameObject.SetActive(false);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
            rankImages[indexBronzeRankOne].gameObject.SetActive(true);

        }
    }

}
