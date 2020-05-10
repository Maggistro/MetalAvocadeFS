using System.Linq;
using UnityEngine;

namespace Avocado
{
    class IntroEvent : ScriptEvent
    {
        protected override ScriptEventType lastEvent {get; set;} = ScriptEventType.INTRO;

        private float storedMovementSpeed;

        void Start()
        {
            character.SetActivestate = false;
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

        protected override void ExecuteEvent(Step step)
        {
            switch(this.lastEvent) {
                case ScriptEventType.INTRO:
                    if (step.type == ScriptEventType.STEAL_AVOCADO) { // just entered, gonna steal that avocado
                        jester.movementSpeed = 0;
                        character.GetAudioSource.loop = false;
                        character.GetAudioSource.Stop();
                        
                        this.jester.PickupAvocado();
                        jester.GetAudioSource.clip = jester.audioAvocadoCry;
                        jester.GetAudioSource.Play();
                        return;
                    }
                    Debug.Log("Unsupported script event after intro");
                    break;
                case ScriptEventType.STEAL_AVOCADO:
                    if (step.type == ScriptEventType.LEAVE_HUT) { // got out with avocado, player can now move
                        this.character.SetActivestate = true;
                        jester.GetAudioSource.clip = jester.audioIntroCrash;
                        jester.GetAudioSource.Play();
                        return;
                    }
                    Debug.Log("Unsupported script event after steal avocado");
                    break;
                case ScriptEventType.LEAVE_HUT:
                    if (step.type == ScriptEventType.VANDALIZE) { // Move to the vandalize spot and clean up
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