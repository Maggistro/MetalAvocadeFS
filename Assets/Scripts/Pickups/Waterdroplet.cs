using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class Waterdroplet : GenericPickup
    {
        public float speed = 10f;
        public bool bossfightActive = false;
        float direction = 0;
        public void InitBossfight(float x)
        {
            direction = x;
            bossfightActive = true;
            gameObject.tag = "Projectile";
        }
        void Update()
        {
            if (bossfightActive)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime * speed * direction, transform.position.y, transform.position.z);
            }
        }
    }
}