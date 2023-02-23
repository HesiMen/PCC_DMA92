using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceToObject : MonoBehaviour
{
    public Vector3 directonForce;
    public Rigidbody rb;
    public float force = 40f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForceAtPosition(directonForce * force, rb.position , ForceMode.Impulse);
        }
    }

}
