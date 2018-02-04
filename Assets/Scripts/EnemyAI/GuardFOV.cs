using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFOV : MonoBehaviour
{
    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    private Quaternion viewAngle;
    private Vector3 viewDirection;

    private bool canSeePlayer = false;

    private RaycastHit hit;
    private Transform eyes;
    private Guard guard;

    private void Awake()
    {
        eyes = transform.GetChild(0);
        guard = GetComponent<Guard>();

        viewAngle = eyes.transform.rotation * startingAngle;
        viewDirection = viewAngle * Vector3.forward;
    }

    private void Update()
    {
        DrawFOV();
        UpdateGuardState();
    }

    void DrawFOV()
    {
        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(eyes.position, viewDirection, out hit, guard.viewDistance))
            {
                Player player = hit.collider.GetComponent<Player>();
                if (player)
                {
                    Debug.Log("I see the player.");
                    GlobalVariables.instance.detectionMeterValue += 0.6f;
                }
            }

            viewDirection = stepAngle * viewDirection;
            Debug.DrawRay(eyes.transform.position, viewDirection * guard.viewDistance, Color.red);
        }
    }

    void UpdateGuardState()
    {
        if(GlobalVariables.instance.detectionMeterValue >= GlobalVariables.instance.maxDetectionMeterValue)
        {
            GlobalVariables.instance.detectionMeterValue = GlobalVariables.instance.maxDetectionMeterValue;
            guard.state = Guard.State.CHASE;
            Debug.Log("Chasing player");
        }

        if (GlobalVariables.instance.detectionMeterValue <= 0) GlobalVariables.instance.detectionMeterValue = 0;
    }
}
