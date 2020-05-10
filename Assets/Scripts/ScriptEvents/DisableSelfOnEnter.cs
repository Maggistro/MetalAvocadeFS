using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class DisableSelfOnEnter : MonoBehaviour
    {
        void OnTriggerEnter(Collider collider)
        {
            if (collider.GetComponentInParent<CharacterController>()) {
                gameObject.SetActive(false);
            }
        }
    }
}
