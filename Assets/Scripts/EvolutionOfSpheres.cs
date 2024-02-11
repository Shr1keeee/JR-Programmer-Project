using System.Collections;
using UnityEngine;

public class EvolutionOfSpheres : MonoBehaviour
{
    [SerializeField] bool spawnNewBall;
    public int pointValueForEvolvedSphereA;

    [SerializeField] GameObject evolvedBalls;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        spawnNewBall = true;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    //
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            //��� ������������ ���� ������������� �������� false ��� ���������� spawnNewBall
            if (spawnNewBall)
            {
                collision.gameObject.GetComponent<EvolutionOfSpheres>().spawnNewBall = false;
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
            Instantiate(evolvedBalls, transform.position, Quaternion.identity);
        }
        //�������� ����� �������� ������������
        Destroy(gameObject);
        //���������� �����
        gameManager.UpdateScore(pointValueForEvolvedSphereA);
        yield return null;
    }

}
