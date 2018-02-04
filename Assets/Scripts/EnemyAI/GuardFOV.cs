using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFOV : MonoBehaviour
{
    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(3, Vector3.up);
    private Vector3 viewDirection;

    private bool canSeePlayer = false;

    private Transform eyes;
    private Transform target;
    private Guard guard;

    private void Start()
    {
        eyes = transform.GetChild(0);
        guard = GetComponent<Guard>();
        target = FindObjectOfType<Player>().transform;

        StartCoroutine("FindTargetsWithDelay", 2f);
    }

    private void Update()
    {
        CheckForPlayer();
        UpdateGuardState();
    }

    void CheckForPlayer()
    {
        Vector3 directionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(directionToTarget, transform.forward);

        if(angleToTarget < 30f)
        {
            canSeePlayer = true;
            Debug.Log("Can see the player");
        } else
        {
            canSeePlayer = false;
        }
    }

    void UpdateGuardState()
    {
        //if (detectionMeter <= 0) detectionMeter = 0;

        if (!canSeePlayer && GlobalVariables.instance.detectionMeterValue >= 0.01)
            GlobalVariables.instance.detectionMeterValue -= 1 * Time.deltaTime;
        else
            GlobalVariables.instance.detectionMeterValue += 1 * Time.deltaTime;

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

    #region Field Of View

    [Range(0, 360)]
    public float viewAngle;
    public float viewRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform targets = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.Log("Can see target");
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    #endregion
}
