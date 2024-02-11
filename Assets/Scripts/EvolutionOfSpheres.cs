using System.Collections;
using UnityEngine;

public class EvolutionOfSpheres : MonoBehaviour
{
    [SerializeField] bool spawnNewBall;
    protected int _pointValueForEvolvedGreenSphere;
    public int pointValueForEvolvedGreenSphere
    {
        get { return _pointValueForEvolvedGreenSphere; }
        set { _pointValueForEvolvedGreenSphere = value; }
    }

    [SerializeField] GameObject evolvedBalls;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        ChangeScoreSpheres();
        spawnNewBall = true;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    protected void OnCollisionEnter(Collision collision)
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
        gameManager.UpdateScore(pointValueForEvolvedGreenSphere);
        yield return null;
    }

    private void ChangeScoreSpheres()
    {
        if (gameObject.CompareTag("SphereA_Evolv_Green") || gameObject.CompareTag("SphereA_Green") || gameObject.CompareTag("SphereA_Last_Evolve_Green"))
        {
            _pointValueForEvolvedGreenSphere = 1000;
        }
    }

}
