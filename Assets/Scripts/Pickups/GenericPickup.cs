using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class GenericPickup : MonoBehaviour
    {
        [SerializeField] private float value;
        public float Value
        {
            get { return value; }
            set
            {
                this.value += value;
                if (this.value <= 0)
                    Destroy(this.gameObject);
            }
        }
    }
}