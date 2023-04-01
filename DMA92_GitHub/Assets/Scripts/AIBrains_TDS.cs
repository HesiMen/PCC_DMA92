﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrains_TDS : MonoBehaviour
{


    public enum AIStates { Idle, Roam, Patrol, Chase, None }
    public NavMeshAgent agent;
    public AIStates currentState = AIStates.None;
    private AIStates prevState;

    public float idleR = 2f;
    private Vector3 startPoint;

    public Vector2 secondsRangeIdle = new Vector2(6f, 8f);
    private float idleSeconds;
    bool foundIdlePoint = false;

    /// <summary>
    /// Idle: Will have an idle animation and may walk a couple of steps in an assigned area.
    /// Go to roam or patrol after x seconds if is on idle
    /// 
    /// Roam : Walk towards a random point on a big assigned area (Radious)  
    /// go back to idle if player is not near
    /// 
    /// 
    /// Patrol: Point A and point B  can be patrolled (for y seconds)
    /// go to idle if player is not near
    /// 
    /// 
    /// Chase : If player withing radious or sight can be chased
    ///     Move towards a point near the player
    /// 
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        idleSeconds = Random.Range(secondsRangeIdle.x, secondsRangeIdle.y);
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        switch (currentState)
        {
            case AIStates.Idle:
                IdleUpdate();
                break;
            case AIStates.Roam:
                RoamUpdate();
                break;
            case AIStates.Patrol:
                PatrolUpdate();
                break;
            case AIStates.Chase:
                ChaseUpdate();
                break;
            case AIStates.None:
                break;
            default:
                break;
        }
    }


    private void ChageState(AIStates toChange)
    {

    }

    private void IdleUpdate()
    {
        Vector3 randPoint = FindPointInArea();

        if (!foundIdlePoint && randPoint != Vector3.zero)
        {


            agent.SetDestination(randPoint);
            foundIdlePoint = true;

        }

        if (foundIdlePoint) // Start counting down
        {
            idleSeconds -= Time.deltaTime;

            if (idleSeconds < 0)
            {
                foundIdlePoint = false;
                idleSeconds = Random.Range(secondsRangeIdle.x, secondsRangeIdle.y);
            }
        }
        // Find a point and go to it. 
        //once reached wait for seconds
        //repeat

    }

    private void RoamUpdate()
    {

    }

    private void PatrolUpdate()
    {

    }

    private void ChaseUpdate()
    {

    }

    private Vector3 FindPointInArea()
    {
        Vector3 randomPoint = startPoint + Random.insideUnitSphere * idleR;
        NavMeshHit hit;
        Vector3 result;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            Debug.DrawLine(agent.transform.position, result, Color.red);
        }
        else
        {
            result = Vector3.zero;

        }
       
        return result;
    }


    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, idleR);
    }
}