using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class ChildStateMan : StateMan, IPatience
    {
        private NavMeshAgent agent;
        [SerializeField] private int patience = 3;
        [SerializeField] private int maxPatience = 3;
        public int Patience 
        {
            get => patience;
            set {
                patience = value;
                if (TaskManager.Instance is not null)
                    TaskManager.Instance.ChildDodged = value;
            }
        }
        public int MaxPatience 
        {
            get => maxPatience;
            set {
                maxPatience = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }

    }
}