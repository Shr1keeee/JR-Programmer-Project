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

    //��������� ������ ����� ������� �� ������ Start
    void Start()
    {
        //����������� ������ ��� ������������, ���� ��� �� ���� �������
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
        //���������� ������ �� ������ �����
        if (Input.GetButtonDown("Submit"))
        {
            //���������� ����� ������
            userName = inputUserName.text;
            //���������� ���������� �����;
            MainManager.Instance.SetUserName(userName);
            //���������� ������ �����
            inputUserName.gameObject.SetActive(false);
            //��������� �����������
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;

            isEnterUserName = true;

        }

    }

    //������� � ������� �������
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

    //������� �� ������� ������� �������� � �������� ����
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

    //��������� �����
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    //��������� �������� �������
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    //���/���� ������������� �����
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //���������� ��������� ���������� � ����������
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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

}
