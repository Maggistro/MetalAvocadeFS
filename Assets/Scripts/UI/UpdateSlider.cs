using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Avocado
{
    public class UpdateSlider : MonoBehaviour
    {
        [SerializeField] private CharacterController player;
        [SerializeField] private Slider slider;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            slider = GetComponent<Slider>();
        }
        public int switchCase;
        void Update()
        {
            switch (switchCase)
            {
                case 0:
                    slider.value = player.WaterLevel;
                    break;
                case 1:
                    slider.value = player.StaminaLevel;
                    break;
            }
        }
    }
}