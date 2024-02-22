using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{

    private float distanceOfRay = 11.0f;
    private bool isActiveClickButton;
    private int indexSphere;
    private bool isNextSpawn;
    private Vector3 scaleClonePrefab = new Vector3(2, 2, 2);
    private Vector3 posClonePrefab = new Vector3(6f, 13f, 2f);

    protected GameObject originalPrefab;
    [SerializeField] GameObject[] spawnedBalls;
    [SerializeField] GameObject clonePrefab;
    [SerializeField] GameObject gameOverTrigger;
    

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject clickHint;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        isActiveClickButton = true;
        isNextSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Создание оригинала экземпляра сферы
        if (!gameManager.isGameOver && isActiveClickButton)
        {
            SpawnAtMousePos();

        }
        //Создания клона экземпляра сферы 
        if (!gameManager.isGameOver && isNextSpawn)
        {
            PreviewInstantiateSphere();

        }

    }

    //Спавн сферы на полжении курсара и по ЛКМ 
    public void SpawnAtMousePos()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //определение положения мыши для представления луча
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //При столкновении луча с Collider возвращается true и спавнится сфера
            if (Physics.Raycast(ray))
            {
                //Создание оригинала экземпляра сферы
                originalPrefab = Instantiate(spawnedBalls[indexSphere], ray.GetPoint(distanceOfRay), Quaternion.identity);
                //Отключение возможности спама ЛКМ
                isActiveClickButton = false;
                //Изменение с false на true для запуска метода создания клона экземпляра сферы
                isNextSpawn = true;
                //Уничтожение клона экземпляра сферы
                Destroy(clonePrefab);

            }
            clickHint.gameObject.SetActive(false);
            StartCoroutine(DelayTooClick());

        }
    }

    //Дилей между нажатиями ЛКМ
    IEnumerator DelayTooClick()
    {
        yield return new WaitForSeconds(1f);
        isActiveClickButton = true;
    }


    //Создание клона экземпляра сферы и его отображение в правом верхнем углу экрана
    public void PreviewInstantiateSphere()
    {
        if (isNextSpawn)
        {
            //Рандомный выбор сферы для создания экземпляров
            indexSphere = Random.Range(0, spawnedBalls.Length);
            //Создание экземпляра клона сферы
            clonePrefab = Instantiate(spawnedBalls[indexSphere], posClonePrefab, Quaternion.identity);
            //Включение Kinematic в RB у клона 
            clonePrefab.GetComponent<Rigidbody>().isKinematic = true;
            //Изменение размера клона
            clonePrefab.transform.localScale = scaleClonePrefab;
            //Отключения спавна
            isNextSpawn = false;
        }
    }
}