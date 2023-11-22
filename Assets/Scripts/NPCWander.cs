using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.AI.Navigation;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

// A very simple script to let a NPC wander
// Deprecated
public class NPCwander : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.isStopped && Vector3.Distance(gameObject.transform.position, agent.destination) < 1)
        {
            Debug.Log("Reached Target");
            StartCoroutine(PickNext());
        }
    }

    IEnumerator PickNext()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(4);
        agent.isStopped = false;
        NavMeshHit hit;
        Vector3 randomTarget = gameObject.transform.position + UnityEngine.Random.insideUnitSphere*10;
        randomTarget.y = gameObject.transform.position.y;

        if (NavMesh.SamplePosition(randomTarget, out hit, 5.0f, NavMesh.AllAreas))
        {
            Debug.Log("Setting new target");
            agent.destination = hit.position;
        }
        else
        {
            Debug.Log("NPC is stuck");
        }
    }
    public Transform goal;
}