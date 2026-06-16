using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private CharacterController _controller;
    public Transform cameraTransform;
    private Animator animator;
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = cameraTransform.TransformDirection(direction);
        direction.y = 0; 

        _controller.Move(direction * (Time.deltaTime*30));
        float move = Input.GetAxis("Horizontal") + Input.GetAxis("Vertical");

        
        if (move != 0)
        {
            animator.SetBool("isWalking", true);  
        }
        else
        {
            animator.SetBool("isWalking", false);  
        }
    }
}

