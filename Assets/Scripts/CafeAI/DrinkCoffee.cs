using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class DrinkCoffee : State
    {
        [SerializeField]
        public State FinishedTransitition;
        [SerializeField]
        public float InteractionReach = 1.5f;

        private ISeesCoffee coffeeMan;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            // require statemanager to see coffee
            if (manager is not ISeesCoffee)
                Debug.LogError("Statemanager needs to see coffee");
            coffeeMan = (ISeesCoffee)manager;
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }

        void Update()
        {
            if (coffeeMan.TargetCoffee is not null &&
                Vector3.Distance(coffeeMan.TargetCoffee.gameObject.transform.position, gameObject.transform.position) < InteractionReach &&
                coffeeMan.TargetCoffee.status == Interactable.Status.Usable)
            {
                coffeeMan.TargetCoffee.status = Interactable.Status.Used;
                Debug.Log("Drank Coffee");
                manager.Transition(this, FinishedTransitition);
            }
            else
            {
                Debug.Log("Failed to drink coffee");
                manager.Transition(this, FinishedTransitition);
            }
        }
    }
}