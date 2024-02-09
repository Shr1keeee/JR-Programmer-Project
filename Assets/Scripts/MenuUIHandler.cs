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

    public string userName;
    public bool isEnterUserName;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI welcomeUserText;

    [SerializeField] TMP_InputField inputUserName;

    //Установка значий после нажатия на кнопку Start
    void Start()
    {
        //Предложение ввести имя пользователя, если оно не было введено
        userName = MainManager.Instance.LoadUserName();
        Debug.Log(userName);
        if (userName == null && !isEnterUserName)
        {
            inputUserName.gameObject.SetActive(true);
        }
        else
        {
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;
            MainManager.Instance.SaveUserName(userName);
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
            //Сохранение введенного имени;
            MainManager.Instance.SaveUserName(userName);
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

}
