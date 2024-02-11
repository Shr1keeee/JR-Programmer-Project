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
        gameManager.UpdateScore(pointValueForEvolvedSphereA);
        yield return null;
    }

}
