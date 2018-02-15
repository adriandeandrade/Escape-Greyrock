using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFOV : MonoBehaviour
{
    private Vector3 viewDirection;
    public LayerMask obstacleMask;

    private bool canSeePlayer = false;
    [SerializeField] private int detectionAmount;
    [SerializeField] public float viewAngle;

    private Transform eyes;
    private Transform target;
    private Guard guard;

    private void Start()
    {
        eyes = transform.GetChild(0);
        guard = GetComponent<Guard>();
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        if (CanSeePlayer()) canSeePlayer = true;
        if (!CanSeePlayer()) canSeePlayer = false;

        UpdateGuardState();
    }

    bool CanSeePlayer()
    {
        Transform target = FindObjectOfType<Player>().transform;
        if(Vector3.Distance(transform.position, target.position) < guard.viewDistance)
        {
            Vector3 directionToPlayer = (target.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if(angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if(!Physics.Linecast(transform.position, target.position, obstacleMask))
                {
                    return true;
                }
            } 
        }
        return false;
    }

    void UpdateGuardState()
    {
        //if (detectionMeter <= 0) detectionMeter = 0;

        if (!canSeePlayer && GlobalVariables.instance.detectionMeterValue >= 0.01)
            GlobalVariables.instance.detectionMeterValue -= (detectionAmount / 2) * Time.deltaTime;
        else
            GlobalVariables.instance.detectionMeterValue += detectionAmount * Time.deltaTime;

        if (GlobalVariables.instance.detectionMeterValue >= GlobalVariables.instance.maxDetectionMeterValue)
        {
            GlobalVariables.instance.detectionMeterValue = GlobalVariables.instance.maxDetectionMeterValue;
            if (guard.state != Guard.State.CHASE)
            {
                guard.state = Guard.State.CHASE;
                Debug.Log("Chasing player");
            }
        }
    }

//#if UNITY_EDITOR

//    void OnDrawGizmos()
//    {
//        if(Application.isEditor)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawLine(transform.position, transform.forward * guard.viewDistance);
//        }
//    }

//#endif
}
