using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 15f;
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(20f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        if (transform.position.x < -20)
        {
            transform.position = spawnPosition;
        }

    }
}
