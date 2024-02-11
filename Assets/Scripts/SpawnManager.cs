using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] float distanceOfRay = 11.0f;
    public bool isActiveClickButton;
    [SerializeField] private int indexSphere;
    public bool isNextSpawn;
    private Vector3 scaleClonePrefab = new Vector3(2, 2, 2);
    private Vector3 posClonePrefab = new Vector3(9f, 15.32f, 2f);

    public GameObject[] spawnedBalls;
    public GameObject[] evolvedBalls;
    private GameObject clonePrefab;
    public GameObject originalPrefab;
    [SerializeField] GameObject gameOverTrigger;
    

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameManager gameManager;


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
        if (gameManager.isGameActive) //&& isActiveClickButton
        {
            SpawnAtMousePos();

        }
        //Создания клона экземпляра сферы 
        if (gameManager.isGameActive && isNextSpawn)
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

            StartCoroutine(DelayTooClick());

        }
    }

    //Дилей между нажатиями ЛКМ
    IEnumerator DelayTooClick()
    {
        yield return new WaitForSeconds(3f);
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
