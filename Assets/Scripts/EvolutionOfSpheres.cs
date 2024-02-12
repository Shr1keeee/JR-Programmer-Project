using System.Collections;
using UnityEngine;

public class EvolutionOfSpheres : MonoBehaviour
{
    [SerializeField] bool spawnNewBall;
    protected int _pointValueForEvolutionSphere;
    public int PointValueForEvolutionSphere
    {
        get { return _pointValueForEvolutionSphere; }
        set { _pointValueForEvolutionSphere = value; }
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
        gameManager.UpdateScore(PointValueForEvolutionSphere);
        yield return null;
    }

    public virtual void ChangeScoreSpheres()
    {
            PointValueForEvolutionSphere = 0;
    }

}
