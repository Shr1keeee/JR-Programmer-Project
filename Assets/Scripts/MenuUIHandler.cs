using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Audio;
using System.Collections.Generic;

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
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] GameObject settingsScreen;

    Resolution[] resolutions;

    //Установка значий после нажатия на кнопку Start
    void Start()
    {
        //Предложение ввести имя пользователя, если оно не было введено
        userName = MainManager.Instance.GetUserName();
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

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
       
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

    public void ShowSettings()
    {
        titleScreen.gameObject.SetActive(false);
        settingsScreen.gameObject.SetActive(true);

    }

    //Переход из таблицы лидеров настроек в основное меню
    public void BackToMenu()
    {

        if (leaderboardScreen.gameObject.activeInHierarchy == true)
        {
            leaderboardScreen.gameObject.SetActive(false);
        }

        if (settingsScreen.gameObject.activeInHierarchy == true)
        {
            settingsScreen.gameObject.SetActive(false);
        }

        titleScreen.gameObject.SetActive(true);
    }

    //Настройка звука
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    //Настройка качества графики
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    //Вкл/выкл Полноэкранный режим
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Применение выбраного разрешения в настройках
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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
