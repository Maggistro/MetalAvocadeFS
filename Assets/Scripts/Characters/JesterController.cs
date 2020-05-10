using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class JesterController : NpcController
    {
        private Renderer[] renderers;
        private Renderer spriteNormal;
        private Renderer spritePainting;
        public enum JesterState
        {
            NORMAL, VANDALIZING
        }

        private new void Awake()
        {
            base.Awake();
            npcName = "Narr";
            renderers = GetComponentsInChildren<Renderer>();
            FindSprites();
            SetSpriteStates(JesterState.NORMAL);
        }

        private void FindSprites()
        {
            Transform sNormal = transform.Find("SpriteNormal");
            Transform sPainting = transform.Find("SpritePainting");
            if (sNormal != null && sPainting != null)
            {
                spriteNormal = sNormal.GetComponent<Renderer>();
                spritePainting = sPainting.GetComponent<Renderer>();
            }
        }

        public void PickupAvocado()
        {
            Debug.Log("picking up avocado");
        }

        public void Vandalize()
        {
            Debug.Log("Vandalizing stuff");
            SetSpriteStates(JesterState.VANDALIZING);
        }

        private void SetSpriteStates(JesterState newJesterState)
        {
            if (spriteNormal != null && spritePainting != null)
            {
                switch(newJesterState)
                {
                    case JesterState.NORMAL:
                        spriteNormal.enabled = true;
                        spritePainting.enabled = false;
                        break;
                    case JesterState.VANDALIZING:
                        spriteNormal.enabled = false;
                        spritePainting.enabled = true;
                        break;
                }
            }
        }

        public void SetVisibility(bool visible)
        {
            Debug.Log(string.Format("Setting visibility of jester to {0}", visible.ToString()));
            foreach(Renderer r in renderers)
            {
                r.enabled = visible;
            }
        }
    }
}