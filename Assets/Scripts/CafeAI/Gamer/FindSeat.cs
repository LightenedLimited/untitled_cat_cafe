using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class FindSeat : State
    {
        // takes the gamer to the closest seat
        // stretch: can modify for a generic customer AI with minimal effort
        [SerializeField] public State AtSeatTransition;
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
            if (gamerMan.Seat is null)
            {
                Seatable[] seats = UnityEngine.Object.FindObjectsByType<Seatable>(FindObjectsSortMode.None);
                float minDistance = 10000;
                foreach (Seatable s in seats)
                {
                    if (s.occupied == false)
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
                {
                    agent.destination = targetSeat.gameObject.transform.position;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // if we've arrived, go next
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                gamerMan.Seat = targetSeat;
                Debug.Log("Reached Coffee");
                manager.Transition(this, AtSeatTransition);
            }
        }
    }
}