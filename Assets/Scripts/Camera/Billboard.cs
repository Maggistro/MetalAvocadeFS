using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            transform.forward = Camera.main.transform.forward;
            this.GetComponent<SpriteRenderer>().flipX = this.GetComponentInParent<NpcController>().transform.forward.x < 0;
        }
    }
}
