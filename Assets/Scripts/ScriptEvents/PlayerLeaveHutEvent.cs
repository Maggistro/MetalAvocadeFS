using System.Linq;
using UnityEngine;

namespace Avocado
{
    class PlayerLeaveHutEvent : ScriptEvent
    {
        public Transform nextJesterPosition;
        protected override ScriptEventType lastEvent { get; set; } = ScriptEventType.LEAVE_HUT;
        private bool isActive = false;
        private float storedMovementSpeed;

        void Start()
        {
            storedMovementSpeed = jester.movementSpeed;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<CharacterController>() != null && other.GetComponent<Boat>() == null) {
                character.SetActivestate = false;
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

        protected override void ExecuteEvent(Step step)
        {
            switch(this.lastEvent) {
                case ScriptEventType.LEAVE_HUT:
                    if (step.type == ScriptEventType.VANDALIZE) {
                        jester.Vandalize(step.waitTime);
                        jester.movementDirection = Vector3.right;
                        return;
                    }
                    Debug.Log("Unsupported script event after leave hut");
                    break;
                case ScriptEventType.VANDALIZE:
                    if (step.type == ScriptEventType.PICKUP_BROOM) {
                        character.PickupBroom();
                        jester.movementSpeed = 0;
                        //TODO: Set jester to next encounter position
                        jester.transform.position = nextJesterPosition.position;
                        character.SetActivestate = true;
                        character.movementSpeed = 0;
                        GameObject.FindGameObjectWithTag("Backgroundmusic").GetComponent<AudioSource>().Play();
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