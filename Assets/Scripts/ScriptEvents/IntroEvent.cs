using System.Linq;
using UnityEngine;

namespace Avocado
{
    class IntroEvent : ScriptEvent
    {
        protected override bool disableOnStart {get; set;} = true;
        protected override ScriptEventType lastEvent {get; set;} = ScriptEventType.INTRO;

        private float storedMovementSpeed;

        void Start()
        {
            storedMovementSpeed = jester.movementSpeed;
        }

        protected override void AnimateScriptEventTransition()
        {
            Transform target = targets.First().target;
            jester.movementDirection = target.position - jester.transform.position;
            jester.movementSpeed = storedMovementSpeed;
        }

        protected override bool NextTargetCondition()
        {
            return Vector3.Distance(jester.transform.position,  targets.First().target.position) < .2f;
        }

        protected override void ExecuteEvent(ScriptEventType type)
        {
            switch(this.lastEvent) {
                case ScriptEventType.INTRO:
                    if (type == ScriptEventType.STEAL_AVOCADO) { // just entered, gonna steal that avocado
                        jester.movementSpeed = 0;
                        this.jester.PickupAvocado();
                        return;
                    }
                    Debug.Log("Unsupported script event after intro");
                    break;
                case ScriptEventType.STEAL_AVOCADO:
                    if (type == ScriptEventType.LEAVE_HUT) { // got out with avocado, player can now move
                        this.character.isDisabled = false;
                        return;
                    }
                    Debug.Log("Unsupported script event after steal avocado");
                    break;
                case ScriptEventType.LEAVE_HUT:
                    if (type == ScriptEventType.VANDALIZE) { // Move to the vandalize spot and clean up
                        jester.movementSpeed = 0;
                        Destroy(this);
                        return;
                    }
                    Debug.Log("Unsupported script event after steal avocado");
                    break;
                default:
                    Debug.Log("Unsupported script event");
                    break;
            }
        }
    }
}