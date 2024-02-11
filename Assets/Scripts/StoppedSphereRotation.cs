using UnityEngine;

public class StoppedSphereRotation : MonoBehaviour
{
    [SerializeField] Rigidbody sphereRb;
    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        sphereRb.rotation = Quaternion.identity;
    }
}
