using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{


    public bool hasBeenThrown = false;
    [SerializeField] public Rigidbody2D rb2d;
    public Collider2D[] myCols;


    public Action<CharacterController> OnCharacterRelease;
    public Action<CharacterController> OnCharacterDrag;
    public Action<GameObject> OnCollected;


    Vector3 startPoint;
    public float retractSpeed = 2f;
    private bool retractNow = false;

    GameObject currentCollected = null;
    private void OnEnable()
    {
        // rb2d.GetAttachedColliders(myCols);

        startPoint = transform.position;
    }
    private void OnMouseUp()
    {
        OnCharacterRelease?.Invoke(this);
        hasBeenThrown = true;




    }

    private void OnMouseDrag()
    {
        OnCharacterDrag?.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        rb2d.velocity = Vector2.zero;
        rb2d.angularVelocity = 0f;
        retractNow = true;

        if(collision.gameObject.tag == "Rat" || collision.gameObject.tag == "Obs")
        {
            currentCollected = collision.gameObject;
            collision.transform.parent = this.transform;
        }

    }

    private void Update()
    {
        if (retractNow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint, Time.deltaTime * retractSpeed);

            if(Vector3.Distance(transform.position, startPoint)< .1f)
            {
                retractNow = false;
                hasBeenThrown = false;
                rb2d.velocity = Vector2.zero;
                rb2d.angularVelocity = 0f;
                OnCollected?.Invoke(currentCollected);
            }
        }
    }

}
