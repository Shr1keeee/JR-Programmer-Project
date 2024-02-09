using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    private int indexBronzeRankOne = 0;
    private int indexBronzeRankTwo = 1;
    private int indexBronzeRankThree = 2;
    //private int indexSilverRankOne = 3;
    public int score;
    public bool isGameActive;
    public bool isGameOver;
    [SerializeField] int scoreForPreviousSession;
    [SerializeField] int totalScoreForAllSession;
    [SerializeField] int maxScoreBeforeRankBronzeTwo;
    [SerializeField] int maxScoreBeforeRankBronzeThree;

    [SerializeField] GameObject sliderProgressRankBar;
    public List<Image> rankImages;
    public List<Image> currentRank;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button mainMenuButton;




    private void Awake()
    {
        //Обновление Max Value и Min Value у Progress Rank Slider
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
            //актвивация слайдера прогресса
            sliderProgressRankBar.SetActive(true);
            //активация кнопки выхода
            exitButton.gameObject.SetActive(true);

            mainMenuButton.gameObject.SetActive(true);

            isGameActive = false;
            isGameOver = true;
            //актвивация слайдера прогресса
            sliderProgressRankBar.SetActive(true);
            //получение очков за все предыдущие игровые сессии
            scoreForPreviousSession = MainManager.Instance.LoadTotalScoreForAllSession();
            //Добавление очков из текущей сессии к сумме очков из всех предыдущих 
            MainManager.Instance.SaveTotalScoreForAllSession(scoreForPreviousSession, score);
            //получения очков за все игровые сессии
            totalScoreForAllSession = MainManager.Instance.LoadTotalScoreForAllSession();
            //Отображения ранговой прогрессии
            RankPorgression();

            //PlayerPrefs.DeleteAll();
        }
    }

    ////Сохранение очков текущей сессии к сумме предыдущих очков
    //public void SaveTotalScoreForAllSession(int scoreForPreviousSession, int score)
    //{
    //    PlayerPrefs.SetInt("scoreForPreviousSession", scoreForPreviousSession + score);

    //}

    ////Загрузка суммы всех очков за все сессии
    //public int LoadTotalScoreForAllSession()
    //{
    //    return PlayerPrefs.GetInt("scoreForPreviousSession");

    //}

    //Отображение рангов исходя из количество очков
    public void CurrentRank()
    {
        totalScoreForAllSession = MainManager.Instance.LoadTotalScoreForAllSession();
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

    //выход из игры
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
