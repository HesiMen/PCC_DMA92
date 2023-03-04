using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private float distanceFromAnchor = 3f;
    [SerializeField] private Transform anchor;
    [SerializeField] private float _ForceAdded = 200f;
    private Vector3 _MousePos;

    [SerializeField] CharacterController[] allMyCharacters;

    CharacterController currentCharacter;
    bool isHolding = false;

    private void Start()
    {
        foreach (var character in allMyCharacters)
        {

            character.OnCharacterRelease += CharacterRelease;
            character.OnCharacterDrag += CharacterDrag;

        }
    }

    private void CharacterDrag(CharacterController obj)
    {
        if (obj.hasBeenThrown)
            return;
        if (isHolding == false)
        {
            currentCharacter = obj;


            foreach (var character in allMyCharacters)
            {
                if (character != currentCharacter)
                {
                    foreach (var col in character.myCols)
                    {
                        col.enabled = false;
                    }
                }

            }


            foreach (var col in currentCharacter.myCols)
            {
                col.enabled = true;
            }
            isHolding = true;
        }
        currentCharacter.rb2d.bodyType = RigidbodyType2D.Kinematic;
        _MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        currentCharacter.transform.position = anchor.position + Vector3.ClampMagnitude(_MousePos - anchor.position, distanceFromAnchor);

    }

    private void CharacterRelease(CharacterController obj)
    {
        if (obj.hasBeenThrown)
            return;
        currentCharacter.rb2d.bodyType = RigidbodyType2D.Dynamic;
        ThrowCharacter(obj);
        isHolding = false;



        foreach (var character in allMyCharacters)
        {

            foreach (var col in character.myCols)
            {
                col.enabled = true;
            }


        }

    }



    private void ThrowCharacter(CharacterController character)
    {
        Vector3 direction = _MousePos - anchor.position;
        character.rb2d.AddForce(-direction * _ForceAdded);
    }
}
