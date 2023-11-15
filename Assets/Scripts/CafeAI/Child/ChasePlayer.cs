using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class ChasePlayer : State
    {
        [SerializeField] State SuccessTransition;
        [SerializeField] State CloseTransition;
        [SerializeField] State FailTransition;
        [SerializeField] float PathInterval;
        [SerializeField] float ChaseTime;
        [SerializeField] float CloseThreshold;

        private GameObject player;
        private float pathTime;
        private float chaseTimeLeft;
        private NavMeshAgent agent;
        protected override void Start()
        {
            base.Start();
            pathTime = PathInterval;
            chaseTimeLeft = ChaseTime;
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");

            try
            {
                player = GameObject.FindGameObjectsWithTag("Player")[0];
            }
            catch (System.Exception e)
            {
                Debug.LogError("No Player - Tag an object with 'Player'");
            }
        }

        // Update is called once per frame
        void Update()
        {
            pathTime -= Time.deltaTime;
            chaseTimeLeft -= Time.deltaTime;
            if (pathTime < 0 && !agent.pathPending)
            {
                pathTime = PathInterval;
                agent.destination = player.transform.position;
            }
            if (Vector3.Distance(gameObject.transform.position, player.transform.position) < CloseThreshold)
            {
                Debug.Log("Close to Player");
                manager.Transition(this, CloseTransition);
            }
            if (chaseTimeLeft < 0)
            {
                Debug.Log("Did not get close to player :(");
                manager.Transition(this, FailTransition);
            }
            // On collision enter with player, something?
        }
    }
}