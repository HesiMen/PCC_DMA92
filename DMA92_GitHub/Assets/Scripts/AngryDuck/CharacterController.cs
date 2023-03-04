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

    private void OnEnable()
    {
       // rb2d.GetAttachedColliders(myCols);
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


}
