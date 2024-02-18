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

    //��������� ������ ����� ������� �� ������ Start
    void Start()
    {
        //����������� ������ ��� ������������, ���� ��� �� ���� �������
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

    //������� �� ������� ������� � ����
    public void BackToMenu()
    {
        titleScreen.gameObject.SetActive(true);
        leaderboardScreen.gameObject.SetActive(false); 
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
