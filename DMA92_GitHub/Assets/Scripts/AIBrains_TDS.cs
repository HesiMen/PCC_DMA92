using System.Collections;
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
    Vector3 currPoint;
    public float chaseRadius = 4f;

    public float fOffsetChase = 0f;
    Vector3 possiblePlayerPos = Vector3.zero;
    [SerializeField] private Animator animator;
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
        possiblePlayerPos = CheckNearbyPlayer();

        if (Vector2.Distance(new Vector2(agent.transform.position.x, agent.transform.position.z),
            new Vector2(currPoint.x, currPoint.z)) <= .5f)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            animator.SetTrigger("Run");
        }

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

        // Find a point and go to it. 
        //once reached wait for seconds
        //repeat
        Vector3 randPoint = FindPointInArea();

        if (!foundIdlePoint && randPoint != Vector3.zero)
        {

            currPoint = randPoint;
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

        //Debug.Log(agent.transform.position);
        //Debug.Log(currPoint);
        //Debug.Log(agent.pathStatus);
        


    }

    private void RoamUpdate()
    {

    }

    private void PatrolUpdate()
    {

    }

    private void ChaseUpdate()
    {

        if (possiblePlayerPos != Vector3.zero)
        {
            NavMeshHit hit;
           if(NavMesh.SamplePosition(possiblePlayerPos, out hit, 2.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }

    }

    private Vector3 CheckNearbyPlayer()
    {
      

        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(agent.transform.position + agent.transform.forward * fOffsetChase, chaseRadius, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i].GetComponent<PlayerController>() != null)
            {
                Debug.DrawLine(agent.transform.position, hitColliders[i].transform.position, Color.red);
                currentState = AIStates.Chase;
                return hitColliders[i].transform.position;
            }

        }

        currentState = AIStates.Idle;
        return Vector3.zero;
    }

    private Vector3 FindPointInArea()
    {
        Vector3 randomPoint = startPoint + Random.insideUnitSphere * idleR;
        NavMeshHit hit;
        Vector3 result;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            //Debug.DrawLine(agent.transform.position, result, Color.red);
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
        Gizmos.DrawWireSphere(transform.position, idleR);


        Gizmos.color = currentState == AIStates.Chase ? Color.red:Color.green;
        Gizmos.DrawWireSphere(agent.transform.position + agent.transform.forward * fOffsetChase, chaseRadius);

    }
}
