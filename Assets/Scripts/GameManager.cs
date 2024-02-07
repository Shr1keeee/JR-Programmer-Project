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
        //Обновление Max Value и Min Value у Progress Rank Slider
        maxScoreBeforeRankBronzeTwo = 200000;
        maxScoreBeforeRankBronzeThree = 600000;


    }
    // Start is called before the first frame update
    void Start()
    {
        //Предложение ввести имя пользователя, если оно не было введено
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

    //Обновление очков в текущей сессии
    public void UpdateScore(int scoreToAdd)
    {
        if (isGameActive)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }

    }

    //Загрузка сцены после нажатия на кнопку Рестарт
    public void RestartGame() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Условия для Game Over
    public void GameOver(bool isGameOverTrigger)
    {
        if (isGameOverTrigger)
        {
            //активация текста Game Over'a
            gameOverText.gameObject.SetActive(true);
            //активация кнопки перезапуска
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
            isGameOver = true;
            //актвивация слайдера прогресса
            sliderProgressRankBar.SetActive(true);
            //получение очков за все предыдущие игровые сессии
            scoreForPreviousSession = LoadTotalScoreForAllSession();
            //Добавление очков из текущей сессии к сумме очков из всех предыдущих 
            SaveTotalScoreForAllSession(scoreForPreviousSession, score);
            //получения очков за все игровые сессии
            totalScoreForAllSession= LoadTotalScoreForAllSession();
            //Отображения ранговой прогрессии
            RankPorgression();

            //PlayerPrefs.DeleteAll();
        }
    }

    //Установка значий после нажатия на кнопку Start
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

    //Сохранение очков текущей сессии к сумме предыдущих очков
    public void SaveTotalScoreForAllSession(int scoreForPreviousSession, int score)
    {
        PlayerPrefs.SetInt("scoreForPreviousSession", scoreForPreviousSession + score);

    }

    //Загрузка суммы всех очков за все сессии
    public int LoadTotalScoreForAllSession()
    {
        return PlayerPrefs.GetInt("scoreForPreviousSession");

    }

    //Ввод имени игрока
    public void UserName()
    {
        //Сохранения текста из строки ввода
        if (Input.GetButtonDown("Submit"))
        {
            //Сохранение имени игрока
            userName = inputUserName.text;
            SaveUserName(userName);
            //Отключение строки ввода
            inputUserName.gameObject.SetActive(false);
            //Включение приветствия
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;

            isEnterUserName = true;

        }

    }

    //Сохранение имени игрока
    private void SaveUserName(string userName)
    {
        PlayerPrefs.SetString("userName", userName);
    }

    //Загрузка имени игрока
    private string LoadUserName()
    {
        return PlayerPrefs.GetString("userName", "NoName");
    }


    //Отображение рангов исходя из количество очков
    private void CurrentRank()
    {
        totalScoreForAllSession = LoadTotalScoreForAllSession();
        //Отображение ранга Бронза 3
        if (totalScoreForAllSession < maxScoreBeforeRankBronzeTwo)
        {
            currentRank[indexBronzeRankThree].gameObject.SetActive(true);
        }
        else
        {
            currentRank[indexBronzeRankTwo].gameObject.SetActive(true);
        }
    }

    //Отображение прогрессии рангов
    private void RankPorgression()
    {
        //Отображение прогресса рангов бронза 3 -> 2
        if (totalScoreForAllSession < maxScoreBeforeRankBronzeTwo)
        {
            //Присовение новых значений для слайдера прогресса
            sliderProgressRankBar.GetComponent<Slider>().value = totalScoreForAllSession;
            rankImages[indexBronzeRankThree].gameObject.SetActive(true);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
        }
        //Отображение прогресса рангов бронза 2 -> 1
        if (totalScoreForAllSession > maxScoreBeforeRankBronzeTwo)
        {
            //Присовение новых значений для слайдера прогресса
            sliderProgressRankBar.GetComponent<Slider>().minValue = maxScoreBeforeRankBronzeTwo;
            sliderProgressRankBar.GetComponent<Slider>().maxValue = maxScoreBeforeRankBronzeThree;
            sliderProgressRankBar.GetComponent<Slider>().value = totalScoreForAllSession;
            //Перемещение изобращения ранга бронза 2 на позицию бронза 3
            rankImages[indexBronzeRankTwo].gameObject.transform.position = new Vector3(rankImages[indexBronzeRankThree].transform.position.x, rankImages[indexBronzeRankThree].transform.position.y);
            //активация новых рангов и деактивация ранга бронза 
            rankImages[indexBronzeRankThree].gameObject.SetActive(false);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
            rankImages[indexBronzeRankOne].gameObject.SetActive(true);

        }
    }

}
