using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{

    [SerializeField] string userName;
    [SerializeField] bool isEnterUserName;

    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI welcomeUserText;
    [SerializeField] TMP_InputField inputUserName;
    [SerializeField] TextMeshProUGUI enterNameText;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject leaderboardScreen;

    //Установка значий после нажатия на кнопку Start
    void Start()
    {
        //Предложение ввести имя пользователя, если оно не было введено
        userName = MainManager.Instance.GetUserName();
        Debug.Log(userName);
        Debug.Log(isEnterUserName);
        if (userName == "" && !isEnterUserName)
        {
            inputUserName.gameObject.SetActive(true);
            enterNameText.gameObject.SetActive(true);
        }
        else
        {
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;
            MainManager.Instance.SetUserName(userName);
            isEnterUserName = true;

        }
    }

    public void StartGame()
    {
        if (isEnterUserName)
        {
            SceneManager.LoadScene(1);
        }

    }

    public void UserName()
    {
        //Сохранения текста из строки ввода
        if (Input.GetButtonDown("Submit"))
        {
                //Сохранение имени игрока
                userName = inputUserName.text;
                //Сохранение введенного имени;
                MainManager.Instance.SetUserName(userName);
                //Отключение строки ввода
                inputUserName.gameObject.SetActive(false);
                //Включение приветствия
                welcomeUserText.gameObject.SetActive(true);
                welcomeUserText.text = "Welcome " + userName;

                isEnterUserName = true;

        }

    }
    
    //Переход в таблицу лидеров
    public void ShowLeaderboard()
    {
        
        MainManager.Instance.GetLeaderboard();
        titleScreen.gameObject.SetActive(false);
        leaderboardScreen.gameObject.SetActive(true);
    }

    //Переход из таблицы лидеров в меню
    public void BackToMenu()
    {
        titleScreen.gameObject.SetActive(true);
        leaderboardScreen.gameObject.SetActive(false); 
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

}
