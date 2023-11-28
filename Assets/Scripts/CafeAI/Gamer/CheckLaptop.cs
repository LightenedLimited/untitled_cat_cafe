using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class CheckLaptop : State
    {
        [SerializeField] public State FinishedTransitition;
        [SerializeField] public State TiredTransitition;
        [SerializeField] public float InteractionReach = 2f;
        [SerializeField] public float Duration = 1f;

        private float actionTime = 0;
        private GamerStateMan gamerMan;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            // Only the gamer checks her laptop
            if (!TryGetComponent(out gamerMan))
                Debug.LogError("State Manager is not a gamer");
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }

        void OnEnable()
        {
            Debug.Log("Using Laptop");
            actionTime = 0;
            if (gamerMan.Laptop is null)
                gamerMan.GetLaptop();

            if (gamerMan.Laptop is not null &&
                Vector3.Distance(gamerMan.Laptop.transform.position, gameObject.transform.position) <= InteractionReach
                && actionTime == 0)
            {
                if (gamerMan.Laptop.Occupied && gamerMan.Laptop.Occupant.CompareTag("Player"))
                {
                    PlayerController controller;
                    if (gamerMan.Laptop.Occupant.TryGetComponent(out controller))
                    {
                        controller.Yeet();
                        gamerMan.Laptop.Leave();
                        Debug.Log("Scare the cat (TODO)");
                    }
                }
            }
        }

        void Update()
        {
            if (actionTime > Duration)
            {
                if (gamerMan.Alertness > 0)
                    manager.Transition(this, FinishedTransitition);
                else
                    manager.Transition(this, TiredTransitition);
            }
            actionTime += Time.deltaTime;
        }
    }
}