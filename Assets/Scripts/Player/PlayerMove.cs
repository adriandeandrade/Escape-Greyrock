using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float walkSpeed;

    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirectionSide = transform.right * horizontal * walkSpeed;
        Vector3 moveDirectionForward = transform.forward * vertical * walkSpeed;

        characterController.SimpleMove(moveDirectionSide);
        characterController.SimpleMove(moveDirectionForward);
    }
}
