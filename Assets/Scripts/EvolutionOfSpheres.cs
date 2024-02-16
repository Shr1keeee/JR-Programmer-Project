using System.Collections;
using UnityEngine;

public class EvolutionOfSpheres : MonoBehaviour
{
    private bool spawnNewBall;
    private bool isGameOverTrigger;

    protected int _pointValueForEvolutionSphere;
    public int PointValueForEvolutionSphere //ENCAPSULATION 
    {
        get { return _pointValueForEvolutionSphere; }
        set { _pointValueForEvolutionSphere = value; }
    }

    [SerializeField] private float _maxRangeY;

    [SerializeField] GameObject evolvedBalls;
    [SerializeField] GameManager gameManager;

    void Start()
    {
        ChangeScoreSpheres();
        spawnNewBall = true;
        isGameOverTrigger = false;
        _maxRangeY = 13.0f;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SphereA_Last_Evolve_Green") && gameObject.CompareTag("SphereA_Last_Evolve_Green"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(PointValueForEvolutionSphere);
        }
        else if (collision.gameObject.CompareTag("SphereB_Last_Evolv_Blue") && gameObject.CompareTag("SphereB_Last_Evolv_Blue"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(PointValueForEvolutionSphere);
        }
        else
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

            //��� ������������ ���� ����������� �������� ������ �� Y � ���� ��� ���� �������������, �� ��������� ����� ����
            if (collision.gameObject.transform.position.y > _maxRangeY)
            {
                isGameOverTrigger = true;
                //��������� ����� ����
                gameManager.GameOver(isGameOverTrigger);

            }

        }

    }

    //����� ����� �����
    IEnumerator RespawnBall()
    {
        //�������� �������� false ��� ���������� spawnNewBall � ����� ����� �����
        if (!spawnNewBall)
        {
            Instantiate(evolvedBalls, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.UpdateScore(PointValueForEvolutionSphere);
        }
        //�������� ����� �������� ������������
        Destroy(gameObject);
        //���������� �����
        gameManager.UpdateScore(PointValueForEvolutionSphere);
        yield return null;
    }

    public virtual void ChangeScoreSpheres() //POLYMORPHISM 
    {
            PointValueForEvolutionSphere = 0;
    }

}
