using System.Linq;
using UnityEngine;

namespace Avocado
{
    class PlayerLeaveHutEvent : ScriptEvent
    {
        protected override bool disableOnStart { get; set; } = false;
        protected override ScriptEventType lastEvent { get; set; } = ScriptEventType.LEAVE_HUT;
        private bool isActive = false;
        private float storedMovementSpeed;

        void Start()
        {
            storedMovementSpeed = jester.movementSpeed;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<CharacterController>() != null) {
                other.GetComponentInParent<CharacterController>().isDisabled = true;
                targets.First().target.position = character.transform.position;
                isActive = true;
            }
        }

        protected override bool NextTargetCondition()
        {
            return isActive && (Vector3.Distance(character.transform.position,  targets.First().target.position) < .2f);
        }

        protected override void AnimateScriptEventTransition(){
            if (isActive) {
                jester.movementSpeed = storedMovementSpeed;
                Transform target = targets.First().target;
                character.movementDirection = target.position - character.transform.position;
                character.movementSpeed = storedMovementSpeed;
            }
        }

        protected override void ExecuteEvent(ScriptEventType type)
        {
            switch(this.lastEvent) {
                case ScriptEventType.LEAVE_HUT:
                    if (type == ScriptEventType.VANDALIZE) {
                        jester.Vandalize();
                        jester.movementDirection = Vector3.right;
                        return;
                    }
                    Debug.Log("Unsupported script event after leave hut");
                    break;
                case ScriptEventType.VANDALIZE:
                    if (type == ScriptEventType.PICKUP_BROOM) {
                        character.PickupBroom();
                        character.isDisabled = false;
                        character.movementSpeed = 0;
                        Destroy(this);
                        return;
                    };
                    Debug.Log("Unsupported script event after vandalize");
                    break;
                default:
                    Debug.Log("Unsupported script event");
                    break;
            }
        }
    }
}