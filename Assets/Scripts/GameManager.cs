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
            //активация основной кнопки меню
            mainMenuButton.gameObject.SetActive(true);
            isGameOver = true;
            //актвивация слайдера прогресса
            sliderProgressRankBar.SetActive(true);
            //получение очков за все предыдущие игровые сессии
            totalScore = scoreForPreviousSession + score;
            totalScoreText.text = "Total score: " + totalScore;
            totalScoreText.gameObject.SetActive(true);
            userName = MainManager.Instance.GetUserName();
            MainManager.Instance.SetTotalScore(totalScore);
            Debug.Log("Game over total score: " + totalScore);
            MainManager.Instance.Setleadrboard(userName, totalScore);
            //получения очков за все игровые сессии
            //Отображения ранговой прогрессии
            RankPorgression();
        }
    }

    //Отображение рангов исходя из количество очков
    public void CurrentRank() //ABSTRACTION 
    {
        totalScoreForAllSession = scoreForPreviousSession;
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


    public void PauseGame()
    {
        if (!isGameActive && !isGameOver)
        {
            Time.timeScale = 0f;
            // активация кнопки выхода
            exitButton.gameObject.SetActive(true);
            //активация основной кнопки меню
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        if (isGameActive && !isGameOver)
        {
            Time.timeScale = 1f;
            exitButton.gameObject.SetActive(false);
            //активация основной кнопки меню
            mainMenuButton.gameObject.SetActive(false);
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
        if (totalScore < maxScoreBeforeRankBronzeTwo)
        {
            //Присовение новых значений для слайдера прогресса
            sliderProgressRankBar.GetComponent<Slider>().value = totalScore;
            rankImages[indexBronzeRankThree].gameObject.SetActive(true);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
        }
        //Отображение прогресса рангов бронза 2 -> 1
        if (totalScore > maxScoreBeforeRankBronzeTwo)
        {
            //Присовение новых значений для слайдера прогресса
            sliderProgressRankBar.GetComponent<Slider>().minValue = maxScoreBeforeRankBronzeTwo;
            sliderProgressRankBar.GetComponent<Slider>().maxValue = maxScoreBeforeRankBronzeThree;
            sliderProgressRankBar.GetComponent<Slider>().value = totalScore;
            //Перемещение изобращения ранга бронза 2 на позицию бронза 3
            rankImages[indexBronzeRankTwo].gameObject.transform.position = new Vector3(rankImages[indexBronzeRankThree].transform.position.x, rankImages[indexBronzeRankThree].transform.position.y);
            //активация новых рангов и деактивация ранга бронза 
            rankImages[indexBronzeRankThree].gameObject.SetActive(false);
            rankImages[indexBronzeRankTwo].gameObject.SetActive(true);
            rankImages[indexBronzeRankOne].gameObject.SetActive(true);

        }
    }



}
