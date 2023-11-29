using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class FindSeat : State
    {
        // TODO: a lil messy
        // takes the gamer to the closest seat
        // stretch: can modify for a generic customer AI with minimal effort
        [SerializeField] public State AtSeatTransition;
        [SerializeField] public State NoSeatTransition;
        public bool SeatExists = true;
        private GamerStateMan gamerMan;
        private NavMeshAgent agent;
        Seatable targetSeat = null;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
            if (!TryGetComponent<GamerStateMan>(out gamerMan))
                Debug.LogError("No Gamer State Manager");
        }

        void OnEnable()
        {
            Debug.Log("Pathing to Seat");

            agent.enabled = true;
            Seatable[] seats = FindObjectsByType<Seatable>(FindObjectsSortMode.None);
            float minDistance = 10000;
            foreach (Seatable s in seats)
            {
                if (s.Occupied == false)
                {
                    float distance = Vector3.Distance(s.gameObject.transform.position, gameObject.transform.position);
                    if (distance < minDistance)
                    {
                        targetSeat = s;
                        minDistance = distance;
                    }
                }
            }

            if (targetSeat is not null)
                agent.destination = targetSeat.gameObject.transform.position;
            else 
                manager.Transition(this, NoSeatTransition);
        }

        // Update is called once per frame
        void Update()
        {
            // if we've arrived, go next
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                gamerMan.Sit(targetSeat);
                Debug.Log("Reached Coffee");
                manager.Transition(this, AtSeatTransition);
            }
        }
    }
}