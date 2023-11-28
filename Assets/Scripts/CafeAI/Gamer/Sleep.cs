using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// No longer drinks coffee or uses laptop
// Basically a filler state
namespace CatCafeAI
{
    public class Sleep : State
    {
        protected override void Awake()
        {
            base.Awake();
        }
    }
}