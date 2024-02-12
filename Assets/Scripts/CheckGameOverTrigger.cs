using UnityEngine;

public class CheckGameOverTrigger : MonoBehaviour
{
    [SerializeField] private bool isGameOverTrigger;

    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isGameOverTrigger = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        //��� ������ ������������ � ���������, ������������� �������, �� �������� ����������� �������� ��� ��������� ������������
        if (other.gameObject.CompareTag("GameOverTrigger") && !isGameOverTrigger)
        {
            isGameOverTrigger = true;
        }

    }

   
    private void OnTriggerEnter(Collider other)
    {
        //�������� ������� �������� isGameOverTrigger ��� ��������� ������������
        if (other.gameObject.CompareTag("GameOverTrigger") && isGameOverTrigger)
        {
            //��������� ����� ����
            gameManager.GameOver(isGameOverTrigger);
            //���������� ��������
            other.gameObject.SetActive(false);
        }
    }

}
