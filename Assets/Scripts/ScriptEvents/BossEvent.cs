using System.Linq;
using UnityEngine;

namespace Avocado
{
    class BossEvent : ScriptEvent
    {
        [SerializeField]
        public Transform jesterStartPosition;
        protected override ScriptEventType lastEvent { get; set; } = ScriptEventType.BOSS;
        private float storedMovementSpeed;
        private bool isActive = false;
        private int artRemovedStart = 0;

        void Start()
        {
            storedMovementSpeed = 7.5f;
        }

        protected override void AnimateScriptEventTransition()
        {
            if (isActive)
            {
                Transform target = targets.First().target;
                jester.movementDirection = target.position - jester.transform.position;
                jester.movementSpeed = storedMovementSpeed;
            }
        }

        protected override bool NextTargetCondition()
        {
            return isActive && (Vector3.Distance(jester.transform.position, targets.First().target.position) < .2f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<CharacterController>() != null && other.GetComponent<Boat>() == null) {
                isActive = true;
                artRemovedStart = character.artRemoved;
                jester.movementSpeed = 0;
                jester.transform.position = jesterStartPosition.position;
            }
        }

        protected override void ExecuteEvent(Step step)
        {
            switch (this.lastEvent)
            {
                case ScriptEventType.BOSS:
                    if (step.type == ScriptEventType.VANDALIZE) {
                        jester.Vandalize(step.waitTime);
                        return;
                    }
                    Debug.Log("Unsupported script event after leave hut");
                    break;
                case ScriptEventType.VANDALIZE:
                    if (step.type == ScriptEventType.VANDALIZE) {
                        jester.Vandalize(step.waitTime);
                        return;
                    }
                    if (step.type == ScriptEventType.BOSS_WON){
                        isActive = false;
                        character.bossfightActive = true;
                        jester.movementSpeed = 0;
                        StartCoroutine(Camera.main.GetComponent<CamFollowPlayer>().CamTraversion());
                        bool playerWon = character.artRemoved - artRemovedStart >= 8;
                        jester.GiveUp(playerWon);
                        character.PickupAvocado();
                        return;
                    }
                    Debug.Log("Unsupported script event after leave hut");
                    break;
                default:
                    Debug.Log("Unsupported script event");
                    break;
            }
        }
    }
}