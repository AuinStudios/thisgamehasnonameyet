using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiTestV2 : MonoBehaviour
{
    private enum AgentStates
    {
        patrolling,
        following,
        attacking,
        evading,
        die
    }

    private NavMeshAgent agent;
    private Vector3 targetPosition;

    private bool isMoving = false;
    private bool isWaiting = false;

    [SerializeField]
    private AgentStates agentState = AgentStates.patrolling;

    [Header("Agent properties")]
    [SerializeField, Range(1.0f, 15.0f)]
    private float agentVisibility = 5.0f;
    [SerializeField, Range(0.1f, 2.0f)]
    private float distanceThreashold = 1.0f;
    [SerializeField, Range(1.0f, 5.0f)]
    private float speed = 3.0f;

    [Header("Delay between patrols")]
    [SerializeField, Range(0.5f, 5.0f)]
    private float minDelay = 2.5f;
    [SerializeField, Range(6.0f, 10.0f)]
    private float maxDelay = 8.0f;
    private float currentDelay;

    private void Start()
    {
        agent = transform.GetComponent<NavMeshAgent>();

        agent.speed = speed;
    }

    private void Update()
    {
        switch (agentState)
        {
            case AgentStates.patrolling:
                {
                    if (!isMoving)
                    {
                        Debug.Log("Finding new position to patrol.");

                        targetPosition = transform.position + Random.insideUnitSphere * agentVisibility;

                        isMoving = true;
                    }
                    else
                    {
                        agent.SetDestination(targetPosition);

                        if (agent.remainingDistance <= distanceThreashold && !isWaiting)
                        {
                            StartCoroutine(WaitForNextPatrol());
                        }
                    }

                    // TODO: finish switch agent to follow state
                    if (true)
                    {

                    }
                }
                break;
            case AgentStates.following:
                {

                }
                break;
            case AgentStates.attacking:
                {

                }
                break;
            case AgentStates.evading:
                {

                }
                break;
            case AgentStates.die:
                {

                }
                break;
            default:
                break;
        }

        agent.SetDestination(targetPosition);
    }

    private IEnumerator WaitForNextPatrol()
    {
        isWaiting = true;

        currentDelay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(currentDelay);
        
        isMoving = false;
        isWaiting = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, agentVisibility);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceThreashold);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
    }
}
