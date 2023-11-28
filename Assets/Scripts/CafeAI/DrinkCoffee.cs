using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class DrinkCoffee : State
    {
        [SerializeField] public State FinishedTransition;
        [SerializeField] public float InteractionReach = 1.5f;
        [SerializeField] public float DrinkAmount = 0.1f;
        [SerializeField] public float Duration = 1f;

        private float drinkTime = 0;
        private ISeesCoffee coffeeMan;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            // require statemanager to see coffee
            if (manager is not ISeesCoffee)
                Debug.LogError("Statemanager needs to see coffee");
            coffeeMan = (ISeesCoffee)manager;
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }

        void OnEnable()
        {
            drinkTime = 0;
            if (coffeeMan.TargetCoffee is null)
                coffeeMan.TargetCoffee = ISeesCoffee.LookForCoffee(gameObject);
        }

        void Update()
        {
            drinkTime += Time.deltaTime;
            if (drinkTime < Duration) return;

            if (coffeeMan.TargetCoffee is not null &&
                Vector3.Distance(coffeeMan.TargetCoffee.gameObject.transform.position, gameObject.transform.position) <= InteractionReach &&
                coffeeMan.TargetCoffee.Amount > 0)
            {
                coffeeMan.TargetCoffee.Amount -= DrinkAmount;
                Debug.Log("Drank Coffee");
                // Only the gamer benefits from drinking coffee at the moment
                coffeeMan.DrinkCoffee();
                manager.Transition(this, FinishedTransition);
            }
            else
            {
                Debug.Log("Failed to drink coffee");
                manager.Transition(this, FinishedTransition);
            }
        }
    }
}