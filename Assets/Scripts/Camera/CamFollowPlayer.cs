using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Avocado
{
    public class CamFollowPlayer : MonoBehaviour
    {
        [SerializeField] Image blend;
        public Transform camRunning;
        public Transform camBoss;
        public float offset;
        [SerializeField] private Transform player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        int switchCaseCam = 0;
        bool traversionActive = false;
        private void Update()
        {
            // var v3 = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0.0f);

            if (Input.GetKeyDown(KeyCode.K))
                StartCoroutine(CamTraversion());
            if (!traversionActive)
            {
                float newX = Mathf.Clamp(player.position.x, -68, Mathf.Infinity);
                transform.position = new Vector3(newX, player.position.y + offset, transform.position.z);
            }
        }
        [SerializeField] float rotationSpeed = 1f;
        Quaternion newRotation = new Quaternion();
        public IEnumerator CamTraversion()
        {
            while (blend.color.a < 1)
            {
                blend.color = new Color(blend.color.r, blend.color.g, blend.color.b, blend.color.a + Time.deltaTime * 1f);
                yield return null;
            }

            switch (switchCaseCam)
            {
                case 0:
                    while (Mathf.Abs(transform.rotation.x - camBoss.rotation.x) > 0.01f)
                    {
                        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed * Time.deltaTime);
                        yield return new WaitForEndOfFrame();
                    }
                    while (offset < 14)
                    {
                        offset += Time.deltaTime * rotationSpeed;
                        yield return new WaitForEndOfFrame();
                    }
                    offset = 14;
                    switchCaseCam = 1;
                    break;
                case 1:
                    while (Mathf.Abs(transform.rotation.x - camRunning.rotation.x) > 0.01f)
                    {
                        transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed * Time.deltaTime);
                        yield return new WaitForEndOfFrame();
                    }
                    while (offset > 0)
                    {
                        offset -= Time.deltaTime * rotationSpeed;
                        yield return new WaitForEndOfFrame();
                    }
                    offset = 0;
                    switchCaseCam = 0;
                    break;
            }

            while (blend.color.a > 0)
            {
                blend.color = new Color(blend.color.r, blend.color.g, blend.color.b, blend.color.a - Time.deltaTime * 1f);
                yield return null;
            }
            float newX = Mathf.Clamp(player.position.x, -64.5f, Mathf.Infinity);
            transform.position = new Vector3(newX - offset, player.position.y + offset, transform.position.z);
        }
    }
}