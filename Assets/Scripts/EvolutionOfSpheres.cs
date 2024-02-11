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
            //при столкновении сфер присваивается значение false для переменной spawnNewBall
            if (spawnNewBall)
            {
                collision.gameObject.GetComponent<EvolutionOfSpheres>().spawnNewBall = false;
            }

            StartCoroutine("RespawnBall");

        }

    }

    //Спавн новой сферы
    IEnumerator RespawnBall()
    {
        //Проверка значения false для переменной spawnNewBall и спавн новой сферы
        if (!spawnNewBall)
        {
            Instantiate(evolvedBalls, transform.position, Quaternion.identity);
        }
        //удаление обоих объектов столкновения
        Destroy(gameObject);
        //Обновление очков
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
