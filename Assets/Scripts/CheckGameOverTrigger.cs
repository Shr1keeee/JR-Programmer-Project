using UnityEngine;

public class CheckGameOverTrigger : MonoBehaviour
{
    [SerializeField] private bool isGameOverTrigger;

    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isGameOverTrigger = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        //При первом столкновении с триггером, присваивается признак, по которому выполняется проверка при повторном столкновении
        if (other.gameObject.CompareTag("GameOverTrigger") && !isGameOverTrigger)
        {
            isGameOverTrigger = true;
        }

    }

   
    private void OnTriggerEnter(Collider other)
    {
        //Проверка наличия признака isGameOverTrigger при повторном столкновении
        if (other.gameObject.CompareTag("GameOverTrigger") && isGameOverTrigger)
        {
            //Инициация конца игры
            gameManager.GameOver(isGameOverTrigger);
            //отключение триггера
            other.gameObject.SetActive(false);
        }
    }

}
