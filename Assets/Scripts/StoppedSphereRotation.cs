using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppedSphereRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody sphereRb;
    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        sphereRb.rotation = Quaternion.identity;
    }
}
