using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameOverTrigger : MonoBehaviour
{
    public bool isGameOverTrigger;

    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isGameOverTrigger = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        //��� ������ ������������ � ���������, ������������� �������, �� �������� ����������� �������� ��� ��������� ������������
        if (other.gameObject.CompareTag("GameOverTrigger") && isGameOverTrigger == false)
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
