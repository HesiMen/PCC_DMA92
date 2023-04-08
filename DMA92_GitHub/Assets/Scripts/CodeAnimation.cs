using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CodeAnimation : MonoBehaviour
{
    public Vector3 movementDirection = Vector3.zero;
    public float timeToMove = 5f;
    private Vector3 startPoing;
    private Vector3 endPoint;

    private float t = 0;

    public AnimationCurve coolCurve;
    private void Start()
    {
        //startPoing = transform.position;
        //endPoint = transform.position + movementDirection;

        transform.DOMoveX(5f, timeToMove, false).SetEase(Ease.OutBounce);
        transform.DOShakeScale(timeToMove);
    }

    // Update is called once per frame
    //void Update()
    //{

    //    t = t + Time.deltaTime;
    //    float nT = t / timeToMove;

    //    float curveT = coolCurve.Evaluate(nT);

    //    Debug.Log(curveT);
    //    transform.position = Vector3.Lerp(startPoing, endPoint, curveT);
    //}
}
