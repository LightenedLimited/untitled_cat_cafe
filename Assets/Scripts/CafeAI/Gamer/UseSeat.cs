using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class UseSeat : State
    {
        // we are basically just lerping into the seat
        // an animation might be good though
        [SerializeField] public State FinishedTransition;
        [SerializeField] public State NoSeatTransition;
        [SerializeField] public float InteractionReach = 1.5f;
        [SerializeField] public float Duration = 1f;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private Quaternion startDirection;
        private Quaternion endDirection;
        private GamerStateMan gamerMan;
        private float timer;
        private NavMeshAgent agent;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<GamerStateMan>(out gamerMan))
                Debug.LogError("No Gamer Manager");
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
            if (gamerMan.Seat is null)
            {
                Debug.Log("No seat to sit on");
                manager.Transition(this, NoSeatTransition);
            }
            else
            {
                if (Vector3.Distance(transform.position, gamerMan.Seat.transform.position) > InteractionReach)
                {
                    gamerMan.Seat = null;
                    Debug.Log("Seat too far");
                    manager.Transition(this, NoSeatTransition);
                }
            }
        }

        // Update is called once per frame
        void OnEnable()
        {
            agent.enabled = false;
            startPosition = transform.position;
            endPosition = gamerMan.Seat.transform.position +
                gamerMan.Seat.SeatOffset;
            startDirection = transform.rotation;
            endDirection = gamerMan.Seat.transform.rotation;
            timer = 0;
        }

        void OnDisable()
        {
            // agent.enabled = true;
        }
        void Update()
        {
            timer += Time.deltaTime / Duration; 
            float mix = timer * timer * (3 - 2*timer);
            transform.rotation = Quaternion.Lerp(startDirection, endDirection, mix);
            transform.position = Vector3.Lerp(startPosition, endPosition, mix);
            if (timer >= 1)
            {
                manager.Transition(this, FinishedTransition);
            }
        }
    }
}