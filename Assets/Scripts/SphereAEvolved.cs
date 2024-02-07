using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SphereAEvolved : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> evolvedBalls;
    public bool spawnNewBall;
    [SerializeField] int pointValueForEvolvedSphereA;

    [SerializeField] GameManager gameManager;


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
        if (collision.gameObject.CompareTag("SphereA_Green"))
        {
            //��� ������������ ���� ������������� �������� false ��� ���������� spawnNewBall
            if (spawnNewBall)
            {
                collision.gameObject.GetComponent<SphereAEvolved>().spawnNewBall = false;
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
