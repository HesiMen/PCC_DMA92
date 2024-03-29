﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private float distanceFromAnchor = 3f;
    [SerializeField] private Transform anchor;
    [SerializeField] private float _ForceAdded = 200f;
    private Vector3 _MousePos;

    [SerializeField] CharacterControllerDuck[] allMyCharacters;

    CharacterControllerDuck currentCharacter;
    bool isHolding = false;

    public Transform collectedItemsTransform;
    private void Start()
    {
        foreach (var character in allMyCharacters)
        {

            character.OnCharacterRelease += CharacterRelease;
            character.OnCharacterDrag += CharacterDrag;
            character.OnCollected += CollectedItem;

        }
    }

    private void CollectedItem(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.parent = collectedItemsTransform;
            obj.transform.localPosition = Vector3.zero;
        }
    }

    private void CharacterDrag(CharacterControllerDuck obj)
    {
        if (obj.hasBeenThrown == true)
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

    private void CharacterRelease(CharacterControllerDuck obj)
    {
        if (obj.hasBeenThrown == true)
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



    private void ThrowCharacter(CharacterControllerDuck character)
    {
        Vector3 direction = _MousePos - anchor.position;
        character.rb2d.AddForce(-direction * _ForceAdded);
    }
}
