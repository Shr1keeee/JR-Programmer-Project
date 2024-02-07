using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;

[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{

    private string userName;
    public bool isEnterUserName;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI welcomeUserText;

    [SerializeField] TMP_InputField inputUserName;

    //Установка значий после нажатия на кнопку Start
    private void Start()
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

    public void StartGame()
    {

        SceneManager.LoadScene(1);
        //PlayerPrefs.DeleteAll();

    }

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

    //выход из игры
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
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

}
