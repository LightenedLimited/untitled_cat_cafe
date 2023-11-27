using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class FindCoffee : State
    {
        [SerializeField]
        public State AtCoffeeTransition;
        private ISeesCoffee coffeeMan;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            // require statemanager to see coffee
            if (manager is not ISeesCoffee)
                Debug.LogError("Statemanager needs to see coffee");
            else
                coffeeMan = (ISeesCoffee)manager;
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }

        // Update is called once per frame
        void Update()
        {
            if (coffeeMan.TargetCoffee is null)
            {
                Coffee nextCoffee = ISeesCoffee.LookForCoffee(gameObject);
                if (nextCoffee is not null)
                {
                    agent.SetDestination(nextCoffee.gameObject.transform.position);
                    coffeeMan.TargetCoffee = nextCoffee;
                }
                else
                {
                    Debug.Log("No Usable Coffees Left :(");
                    Debug.Log("(TODO) Transition to some other state?");
                    manager.Transition(this, null);
                }
            }
            else
            {
                if (coffeeMan.TargetCoffee.Amount <= 0)
                {
                    coffeeMan.TargetCoffee = null;
                }
                // case done
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    Debug.Log("Reached Coffee");
                    manager.Transition(this, AtCoffeeTransition);
                }
            }
        }
        void OnDisable() { }
    }
}