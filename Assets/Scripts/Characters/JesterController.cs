using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class JesterController : NpcController
    {
        private new void Awake()
        {
            base.Awake();
            npcName = "Narr";
        }

        public void PickupAvocado()
        {
            Debug.Log("picking up avocado");
        }

        public void Vandalize()
        {
            Debug.Log("Vandalizing stuff");
        }
    }
}