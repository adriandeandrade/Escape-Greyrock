using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 4;
    [SerializeField] public int viewDistance;
    private int maxDistance = 10;

    private NavMeshAgent guardAgent;
    [HideInInspector] public enum State { PATROL, CHASE }
    [HideInInspector] public State state;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        guardAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        transform.LookAt(playerTransform);

        if (Vector3.Distance(transform.position, playerTransform.position) <= maxDistance)
        {
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //guardAgent.destination = playerTransform.position;
        }

    }

    void MoveEntity()
    {

    }

}
