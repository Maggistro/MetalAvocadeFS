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
    }
}