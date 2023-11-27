using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class CustomerStateMan : StateMan, ISeesCoffee
    {
        private NavMeshAgent agent;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }
        private Coffee targetCoffee;
        public Coffee TargetCoffee{
            get => targetCoffee;
            set => targetCoffee = value;
        }
    }
}