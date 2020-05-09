using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class JesterController : NpcController
    {
        private Renderer[] renderers; 

        private new void Awake()
        {
            base.Awake();
            npcName = "Narr";
            renderers = GetComponentsInChildren<Renderer>();
        }

        public void PickupAvocado()
        {
            Debug.Log("picking up avocado");
        }

        public void Vandalize()
        {
            Debug.Log("Vandalizing stuff");
        }

        public void SetVisibility(bool visible)
        {
            Debug.Log(string.Format("Setting visibility of jester to {0}", visible.ToString()));
            foreach(Renderer r in renderers)
            {
                r.enabled = visible;
            }
        }
    }
}