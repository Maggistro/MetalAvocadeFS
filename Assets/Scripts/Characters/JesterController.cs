using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class JesterController : NpcController
    {
        private GameObject avocado;

        private new void Awake()
        {
            base.Awake();
            npcName = "Narr";
        }

        new void Update()
        {
            base.Update();
            if (avocado != null) {
                MoveAvocado();
            }
        }

        public void PickupAvocado()
        {
            avocado = GameObject.FindGameObjectsWithTag("Avocado")[0];
        }

        void MoveAvocado()
        {
            avocado.transform.position = Vector3.Lerp(avocado.transform.position, transform.position + (Vector3.up * .75f), .5f);
        }

        public void Vandalize()
        {
            Debug.Log("Vandalizing stuff");
        }
    }
}