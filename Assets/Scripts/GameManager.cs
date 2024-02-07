using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private int indexBronzeRankOne = 0;
    private int indexBronzeRankTwo = 1;
    private int indexBronzeRankThree = 2;
    //private int indexSilverRankOne = 3;
    [SerializeField] int score;
    public bool isGameActive;
    public bool isGameOver;
    public bool isEnterUserName;
    private string userName;
    [SerializeField] int scoreForPreviousSession;
    [SerializeField] int totalScoreForAllSession;
    [SerializeField] int maxScoreBeforeRankBronzeTwo;
    [SerializeField] int maxScoreBeforeRankBronzeThree;

    [SerializeField] GameObject sliderProgressRankBar;
    [SerializeField] GameObject titleScreen;
    public List<Image> rankImages;
    public List<Image> currentRank;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI welcomeUserText;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] Button restartButton;




    private void Awake()
    {
        //���������� Max Value � Min Value � Progress Rank Slider
        maxScoreBeforeRankBronzeTwo = 200000;
        maxScoreBeforeRankBronzeThree = 600000;


    }
    // Start is called before the first frame update
    void Start()
    {
        //����������� ������ ��� ������������, ���� ��� �� ���� �������
        userName = LoadUserName();
        Debug.Log(userName);
        if (userName == "NoName")
        {
            inputUserName.gameObject.SetActive(true);
        }
        else
        {
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;

            isEnterUserName = true;

        }
    }

    // Update is called once per frame
    void Update()
    {

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
            isGameActive = false;
            isGameOver = true;
            //���������� �������� ���������
            sliderProgressRankBar.SetActive(true);
            //��������� ����� �� ��� ���������� ������� ������
            scoreForPreviousSession = LoadTotalScoreForAllSession();
            //���������� ����� �� ������� ������ � ����� ����� �� ���� ���������� 
            SaveTotalScoreForAllSession(scoreForPreviousSession, score);
            //��������� ����� �� ��� ������� ������
            totalScoreForAllSession= LoadTotalScoreForAllSession();
            //����������� �������� ����������
            RankPorgression();

            //PlayerPrefs.DeleteAll();
        }
    }

    //��������� ������ ����� ������� �� ������ Start
    public void StartGame()
    {
        if (isEnterUserName)
        {
            isGameActive = true;
            score = 0;
            UpdateScore(0);
            titleScreen.gameObject.SetActive(false);
            isGameOver = false;
            CurrentRank();
            welcomeUserText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }
        //PlayerPrefs.DeleteAll();

    }

    //���������� ����� ������� ������ � ����� ���������� �����
    public void SaveTotalScoreForAllSession(int scoreForPreviousSession, int score)
    {
        PlayerPrefs.SetInt("scoreForPreviousSession", scoreForPreviousSession + score);

    }

    //�������� ����� ���� ����� �� ��� ������
    public int LoadTotalScoreForAllSession()
    {
        return PlayerPrefs.GetInt("scoreForPreviousSession");

    }

    //���� ����� ������
    public void UserName()
    {
        //���������� ������ �� ������ �����
        if (Input.GetButtonDown("Submit"))
        {
            //���������� ����� ������
            userName = inputUserName.text;
            SaveUserName(userName);
            //���������� ������ �����
            inputUserName.gameObject.SetActive(false);
            //��������� �����������
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;

            isEnterUserName = true;

        }

    }

    //���������� ����� ������
    private void SaveUserName(string userName)
    {
        PlayerPrefs.SetString("userName", userName);
    }

    //�������� ����� ������
    private string LoadUserName()
    {
        return PlayerPrefs.GetString("userName", "NoName");
    }


    //����������� ������ ������ �� ���������� �����
    private void CurrentRank()
    {
        totalScoreForAllSession = LoadTotalScoreForAllSession();
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
