using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.Callbacks;
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
        [SerializeField] public float InteractionReach = 2f;
        [SerializeField] public float Duration = 1f;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private Quaternion startDirection;
        private Quaternion endDirection;
        private GamerStateMan gamerMan;
        private float timer;
        private NavMeshAgent agent;
        private Animator anim; 
        private bool sat = false;
        private Rigidbody rb; 
        private CapsuleCollider collider; 
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<GamerStateMan>(out gamerMan))
                Debug.LogError("No Gamer Manager");
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
            if (!TryGetComponent<Rigidbody>(out rb))
                Debug.LogError("No RB");
            if (!TryGetComponent<CapsuleCollider>(out collider))
                Debug.LogError("No collider");
            anim = GetComponent<Animator>(); 
        }

        // Update is called once per frame
        void OnEnable()
        {
            agent.enabled = false;
            startPosition = transform.position;
            endPosition = gamerMan.Seat.SeatPosition;
            startDirection = transform.rotation;
            endDirection = gamerMan.Seat.transform.rotation;
            rb.isKinematic = true;
            timer = 0;
            sat = false;
            collider.height = 0;
            agent.height = 0.5f;
        }

        void OnDisable()
        {
            // agent.enabled = true;
            // collider.height = 2;
            // agent.height = 2f;
        }
        void Update()
        {
            timer += Time.deltaTime; 
            float mixTimer = Math.Clamp(timer, 0, 1);
            float mix = mixTimer * mixTimer * (3 - 2*mixTimer);
            transform.rotation = Quaternion.Lerp(startDirection, endDirection, mix);
            transform.position = Vector3.Lerp(startPosition, endPosition, mix);
            if (timer >= 3)
            {
                manager.Transition(this, FinishedTransition);
                rb.isKinematic = false;
            }
            else if (timer >= 1 && !sat)
            {
                sat = true;
                SitDown();
            }
        }

        void SitDown()
        {
            if (gamerMan.Seat is null)
            {
                Debug.Log("No seat to sit on");
                manager.Transition(this, NoSeatTransition);
            }
            else if (Vector3.Distance(transform.position, gamerMan.Seat.transform.position) > InteractionReach)
            {
                Debug.Log("Seat too far");
                manager.Transition(this, NoSeatTransition);
            }
            else if (!gamerMan.Seat.TrySit(gameObject))
            {
                Debug.Log("Seat occupied");
            }
            else 
            {
                anim.SetTrigger("sitting"); 
                Debug.Log("Sat down");
            }
        }
    }
}