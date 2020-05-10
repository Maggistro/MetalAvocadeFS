using System.Linq;
using UnityEngine;

namespace Avocado
{
    class BossEvent : ScriptEvent
    {
        protected override ScriptEventType lastEvent { get; set; } = ScriptEventType.BOSS;
        private float storedMovementSpeed;
        void Start()
        {
            storedMovementSpeed = jester.movementSpeed;
        }

        protected override void AnimateScriptEventTransition()
        {
            Transform target = targets.First().target;
            jester.movementDirection = target.position - jester.transform.position;
            // jester.movementSpeed = storedMovementSpeed;
        }

        protected override bool NextTargetCondition()
        {
            // return Vector3.Distance(jester.transform.position, targets.First().target.position) < .2f;
            return false;
        }
        public void Trigger(ScriptEventType type)
        {
            ExecuteEvent(type);
        }
        protected override void ExecuteEvent(ScriptEventType type)
        {
            switch (this.lastEvent)
            {
                case ScriptEventType.BOSS:
                    if (type == ScriptEventType.BOSS)
                    { // just entered, gonna steal that avocado
                        character.bossfightActive = true;
                        character.transform.position = new Vector3(transform.position.x, 1, 0);
                        StartCoroutine(Camera.main.GetComponent<CamFollowPlayer>().CamTraversion());
                        return;
                    }
                    Debug.Log("Unsupported script event after boss");
                    break;
                default:
                    Debug.Log("Unsupported script event");
                    break;
            }
        }
    }
}