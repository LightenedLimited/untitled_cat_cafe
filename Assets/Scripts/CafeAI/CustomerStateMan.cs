using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    interface ISeesCoffee
    {
        // public List<GameObject> 
        Interactable TargetCoffee
        {
            get;
            set;
        }

        public static Interactable LookForCoffee(GameObject g) {
            Interactable[] coffees = UnityEngine.Object.FindObjectsOfType<Interactable>();
            if (coffees.Length == 0)
            {
                return null;
            }
            float minDistance = 10000;
            Interactable closestCoffee = null;
            foreach (Interactable c in coffees)
            {
                if (c.status == Interactable.Status.Usable)
                {
                    Transform coffeeTransform = c.gameObject.transform;
                    float distance = Vector3.Distance(coffeeTransform.position, g.transform.position);
                    if (distance < minDistance)
                        closestCoffee = c;
                }
            }
            return closestCoffee;
        }
    }
    public class CustomerStateMan : StateMan, ISeesCoffee
    {
        private NavMeshAgent agent;
        void Start()
        {
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }
        private Interactable _targetCoffee;
        public Interactable TargetCoffee{
            get => _targetCoffee;
            set => _targetCoffee = value;
        }
    }
}