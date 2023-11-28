using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCafeAI
{
    // provides vision between states for nearby coffee objects
    interface ISeesCoffee
    {
        // public List<GameObject> 
        Coffee TargetCoffee
        {
            get;
            set;
        }

        public static Coffee LookForCoffee(GameObject g) {
            Coffee[] coffees = UnityEngine.Object.FindObjectsByType<Coffee>(FindObjectsSortMode.None);
            if (coffees.Length == 0)
            {
                return null;
            }
            float minDistance = 10000;
            Coffee closestCoffee = null;
            foreach (Coffee c in coffees)
            {
                if (c.Amount > 0)
                {
                    Transform coffeeTransform = c.gameObject.transform;
                    float distance = Vector3.Distance(coffeeTransform.position, g.transform.position);
                    if (distance < minDistance)
                    {
                        closestCoffee = c;
                        minDistance = distance;
                    }
                }
            }
            return closestCoffee;
        }
        public void DrinkCoffee();
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
        }
    }
}