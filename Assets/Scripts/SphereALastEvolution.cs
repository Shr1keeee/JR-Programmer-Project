using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class SphereALastEvolution : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> evolvedBalls;
    [SerializeField] bool spawnNewBall;
    [SerializeField] GameManager gameManager;
    [SerializeField] int pointValueForEvolvedSphereA;

    void Start()
    {
        spawnNewBall = true;
        pointValueForEvolvedSphereA = 1000;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SphereA_Evolv_Green"))
        {
            //��� ������������ ���� ������������� �������� false ��� ���������� spawnNewBall
            if (spawnNewBall)
            { 
                collision.gameObject.GetComponent<SphereALastEvolution>().spawnNewBall = false;
            }

            StartCoroutine("RespawnBall");
        }

    }

    //����� ����� �����
    IEnumerator RespawnBall()
    {
        //�������� �������� false ��� ���������� spawnNewBall � ����� ����� �����
        if (!spawnNewBall)
        {
            Instantiate(evolvedBalls[0], transform.position, Quaternion.identity);
        }
        //�������� ����� �������� ������������
        Destroy(gameObject);
        //���������� �����
        gameManager.UpdateScore(pointValueForEvolvedSphereA);
        yield return null;
    }

}
