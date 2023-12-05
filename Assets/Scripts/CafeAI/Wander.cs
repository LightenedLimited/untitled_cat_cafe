using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class Wander : State
    {
        [SerializeField] float ExitProbability = 0.25f;
        [SerializeField] State ExitTransition;
        [SerializeField] float DistanceLimit = 10f;
        [SerializeField] float WaitTime = 2f;
        private NavMeshAgent agent;
        private float WaitTimer;

        private Animator anim; 

        protected override void Awake()
        {
            base.Awake();
            WaitTimer = 0;
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");

            anim = GetComponent<Animator>(); 
        }

        void OnEnable()
        {
            WaitTimer = 0;
            agent.enabled = true;
            PickNext();
            anim.SetBool("walking", true);
            Debug.Log("walking on state"); 
        }
        void OnDisable()
        {
            anim.SetBool("walking", false);
            Debug.Log("walking off state");
        }

        void Update()
        {
            if(agent.pathPending || agent.remainingDistance <= agent.stoppingDistance || agent.isStopped)
            {
                anim.SetBool("walking", false); 
            }
            else
            {
                anim.SetBool("walking", true);
            }
            if (agent.enabled && (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance || agent.isStopped))
            {
                WaitTimer += Time.deltaTime;
                if (WaitTimer > WaitTime)
                {
                    WaitTimer = 0;
                    Debug.Log("Wander Reached Target (or stopped)");
                    PickNext();
                }
            }
        }
        void PickNext()
        {
            if (UnityEngine.Random.value < ExitProbability)
            {
                manager.Transition(this, ExitTransition);
            }
            else
            {
                NavMeshHit hit;
                Debug.Log("Setting new target");
                Vector3 randomTarget = gameObject.transform.position + UnityEngine.Random.insideUnitSphere * DistanceLimit;
                randomTarget.y = gameObject.transform.position.y;
                if (NavMesh.SamplePosition(randomTarget, out hit, DistanceLimit/2, NavMesh.AllAreas))
                    agent.destination = hit.position;
                else
                    Debug.Log("Failed to find new target");
            }
        }
    }
}