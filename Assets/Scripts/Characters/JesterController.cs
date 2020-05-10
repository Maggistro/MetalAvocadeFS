using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class JesterController : NpcController
    {
        [Header("Sound")]
        [SerializeField] AudioSource audioSource; 
        public AudioSource GetAudioSource { get { return audioSource; } } 
        [SerializeField] AudioClip[] audioPaintbrush;  
        [SerializeField] public AudioClip audioIntroCrash;
        [SerializeField] public AudioClip audioAvocadoCry;
        public GameObject graffiti;
        private GameObject avocado;
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

        new void Update()
        {
            base.Update();
            if (avocado != null) {
                MoveAvocado();
            }
        }

        public void PickupAvocado()
        {
            avocado = GameObject.FindGameObjectsWithTag("Avocado")[0];
        }

        void MoveAvocado()
        {
            avocado.transform.position = Vector3.Lerp(avocado.transform.position, transform.position + (Vector3.up * .75f), .5f);
        }

        public void Vandalize()
        {
            Debug.Log("Vandalizing stuff");
            SetSpriteStates(JesterState.VANDALIZING);
            Vector3 position = transform.position;
            position.y = 0.1f;
            Instantiate(graffiti, position, new Quaternion());
            audioSource.clip = audioPaintbrush[Random.Range(0,audioPaintbrush.Length)];
            audioSource.Play();

        }

        private void SetSpriteStates(JesterState newJesterState)
        {
            if (spriteNormal != null && spritePainting != null)
            {
                switch (newJesterState)
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
            foreach (Renderer r in renderers)
            {
                r.enabled = visible;
            }
        }
    }
}