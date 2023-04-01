using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObjectMove : MonoBehaviour
{
    public float delta = 2f; 
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(mousePos);


        transform.rotation = Quaternion.Euler(transform.rotation.x + (mousePos.y* delta), 0f, transform.rotation.z + (mousePos.x * delta));
    }
}
