using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCafeAI
{
    // provides vision between states for nearby coffee objects
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

    // Provides a tracker for how much patience a NPC has left (Child, Customer)
    interface IPatience
    {
        int Patience
        {
            get;
            set;
        }
        int MaxPatience
        {
            get;
            set;
        }
    }
}