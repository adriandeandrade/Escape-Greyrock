using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public Transform playerBody;

    public float mouseSensitivity;

    float xAxisClamp = 0;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationAmountX = mouseX * mouseSensitivity;
        float rotationAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotationAmountY;

        Vector3 targetRotationCamera = transform.rotation.eulerAngles;
        Vector3 targetRotationBody = playerBody.rotation.eulerAngles;

        targetRotationCamera.x -= rotationAmountY;
        targetRotationCamera.z = 0;
        targetRotationBody.y += rotationAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotationCamera.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotationCamera.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotationCamera);
        playerBody.rotation = Quaternion.Euler(targetRotationBody);
    }

}
