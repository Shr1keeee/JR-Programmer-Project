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

    //��������� ������ ����� ������� �� ������ Start
    void Start()
    {
        //����������� ������ ��� ������������, ���� ��� �� ���� �������
        userName = MainManager.Instance.LoadUserName();
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

    }

    public void UserName()
    {
        //���������� ������ �� ������ �����
        if (Input.GetButtonDown("Submit"))
        {
            //���������� ����� ������
            userName = inputUserName.text;
            //���������� ���������� �����;
            MainManager.Instance.SaveUserName(userName);
            //���������� ������ �����
            inputUserName.gameObject.SetActive(false);
            //��������� �����������
            welcomeUserText.gameObject.SetActive(true);
            welcomeUserText.text = "Welcome " + userName;

            isEnterUserName = true;

        }

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
