using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorExample : MonoBehaviour
{
    //public LineRenderer lineA;
    //public LineRenderer lineB;
    //public LineRenderer lineC;

    public Vector3 vectorA;
    public Vector3 vectorB;

    public Vector3 vectorC;

    public float speed = 2f;
    private void Start()
    {
        vectorC = vectorA + vectorB;

        //lineA.SetPosition(0, Vector3.zero);
        //lineA.SetPosition(1, vectorA);
        //lineB.SetPosition(0, vectorA);
        //lineB.SetPosition(1, vectorC);
        //lineC.SetPosition(0, Vector3.zero);
        //lineC.SetPosition(1, vectorC);



    }

    private void Update()
    {
        Debug.DrawLine(Vector3.zero, vectorA, Color.red);
        Debug.DrawLine(Vector3.zero, vectorB, Color.green);
        Debug.DrawLine(Vector3.zero, vectorC, Color.yellow);

        Debug.Log(vectorC.magnitude);
        Debug.Log(Vector3.Distance(vectorA, vectorB));

        //this.transform.position = Vector3.MoveTowards(transform.position, vectorC, Time.deltaTime * speed);
        float incrementTime = Time.time;
        Debug.Log(Mathf.PingPong(incrementTime, vectorC.x), this.gameObject);
        transform.position = new Vector3(Mathf.PingPong(incrementTime, vectorC.x), Mathf.PingPong(incrementTime, vectorC.y), Mathf.PingPong(incrementTime, vectorC.z));
    }
}
