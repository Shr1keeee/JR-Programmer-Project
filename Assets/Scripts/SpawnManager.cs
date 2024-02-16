using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] float distanceOfRay = 11.0f;
    public bool isActiveClickButton;
    [SerializeField] private int indexSphere;
    public bool isNextSpawn;
    private Vector3 scaleClonePrefab = new Vector3(2, 2, 2);
    private Vector3 posClonePrefab = new Vector3(6f, 13f, 2f);

    [SerializeField] GameObject[] spawnedBalls;
    [SerializeField] GameObject clonePrefab;
    protected GameObject originalPrefab;
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
        //�������� ��������� ���������� �����
        if (gameManager.isGameActive && isActiveClickButton) // && isActiveClickButton
        {
            SpawnAtMousePos();

        }
        //�������� ����� ���������� ����� 
        if (gameManager.isGameActive && isNextSpawn)
        {
            PreviewInstantiateSphere();

        }

    }

    //����� ����� �� �������� ������� � �� ��� 
    public void SpawnAtMousePos()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //����������� ��������� ���� ��� ������������� ����
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //��� ������������ ���� � Collider ������������ true � ��������� �����
            if (Physics.Raycast(ray))
            {
                //�������� ��������� ���������� �����
                originalPrefab = Instantiate(spawnedBalls[indexSphere], ray.GetPoint(distanceOfRay), Quaternion.identity);
                //���������� ����������� ����� ���
                isActiveClickButton = false;
                //��������� � false �� true ��� ������� ������ �������� ����� ���������� �����
                isNextSpawn = true;
                //����������� ����� ���������� �����
                Destroy(clonePrefab);

            }

            StartCoroutine(DelayTooClick());

        }

        //foreach (UnityEngine.Touch touch in Input.touches)
        //{
        //    if (touch.phase == UnityEngine.TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        //        //��� ������������ ���� � Collider ������������ true � ��������� �����
        //        if (Physics.Raycast(ray))
        //        {
        //            //�������� ��������� ���������� �����
        //            originalPrefab = Instantiate(spawnedBalls[indexSphere], ray.GetPoint(distanceOfRay), Quaternion.identity);
        //            //���������� ����������� ����� ���
        //            isActiveClickButton = false;
        //            //��������� � false �� true ��� ������� ������ �������� ����� ���������� �����
        //            isNextSpawn = true;
        //            //����������� ����� ���������� �����
        //            Destroy(clonePrefab);

        //        }

        //        StartCoroutine(DelayTooClick());

        //    }
        //}
    }

    //����� ����� ��������� ���
    IEnumerator DelayTooClick()
    {
        yield return new WaitForSeconds(1f);
        isActiveClickButton = true;
    }


    //�������� ����� ���������� ����� � ��� ����������� � ������ ������� ���� ������
    public void PreviewInstantiateSphere()
    {
        if (isNextSpawn)
        {
            //��������� ����� ����� ��� �������� �����������
            indexSphere = Random.Range(0, spawnedBalls.Length);
            //�������� ���������� ����� �����
            clonePrefab = Instantiate(spawnedBalls[indexSphere], posClonePrefab, Quaternion.identity);
            //��������� Kinematic � RB � ����� 
            clonePrefab.GetComponent<Rigidbody>().isKinematic = true;
            //��������� ������� �����
            clonePrefab.transform.localScale = scaleClonePrefab;
            //���������� ������
            isNextSpawn = false;
        }
    }
}