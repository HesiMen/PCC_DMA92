using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;
    [SerializeField] private float characterSpeed = 5f;
    [SerializeField] private float gravity = 9f;
    [SerializeField] Camera camera;
    public bool isWorldMovement = false;
    private float downSpeed;
    void Update()
    {

        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 vMove = isWorldMovement ? Vector3.forward : transform.forward;
        Vector3 hMove = isWorldMovement ? Vector3.right : transform.right;

        
        downSpeed -= characterController.isGrounded ? 0f : gravity * Time.deltaTime;
        Vector3 downMove = Vector3.up * downSpeed;

        #region long If
        //if(isWorldMovement == true)
        //{
        //    vMove = Vector3.forward;
        //}
        //else
        //{
        //    vMove = transform.forward;
        //}
        #endregion
        Vector3 move = vMove * verticalInput + hMove * horizontalInput;


        if(move == Vector3.zero)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Run");
        }

        characterController.Move(move  * characterSpeed * Time.deltaTime + downMove);


        RaycastHit mouseRay;
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out mouseRay))
        {
            transform.LookAt(new Vector3( mouseRay.point.x, transform.position.y, mouseRay.point.z));
            Debug.DrawLine(camera.ScreenToWorldPoint(Input.mousePosition), mouseRay.point, Color.green);
        }

        
    }
}
