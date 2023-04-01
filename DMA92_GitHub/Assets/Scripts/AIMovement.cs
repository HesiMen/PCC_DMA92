using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [SerializeField] Transform pointToMove;
    [SerializeField] NavMeshAgent agent;

    private void Start()
    {
        agent.SetDestination(pointToMove.position);
    }
}
