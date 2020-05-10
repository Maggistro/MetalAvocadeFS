using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Avocado
{
    abstract class ScriptEvent : MonoBehaviour
    {
        [Serializable]
        public struct Step
        {
            public ScriptEventType type;
            public Transform target;
            public float waitTime;
        }

        public List<Step> targets;

        protected abstract ScriptEventType lastEvent  { get; set; }

        public JesterController jester;
        public CharacterController character;

        private bool switchingTarget = false;

        void Update()
        {
            if (NextTargetCondition()) {
                if (!switchingTarget) {
                    switchingTarget = true;
                    StartCoroutine("SelectNextTarget");
                }
            } else {
                AnimateScriptEventTransition();
            }
        }


        void UpdateJesterMovement()
        {
            Transform target = targets.First().target;
            this.jester.movementDirection = target.position - jester.transform.position;
        }


        protected IEnumerator SelectNextTarget()
        {
            ExecuteEvent(targets.First().type);
            yield return new WaitForSeconds(targets.First().waitTime);
            lastEvent = targets.First().type;
            targets.Remove(targets.First());
            switchingTarget = false;
        }

        protected abstract void AnimateScriptEventTransition();
        protected abstract bool NextTargetCondition();
        protected abstract void ExecuteEvent(ScriptEventType type);
    }
}