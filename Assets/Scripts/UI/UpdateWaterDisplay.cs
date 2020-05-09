using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Avocado
{
    public class UpdateWaterDisplay : MonoBehaviour
    {
        [SerializeField] private CharacterController player;
        [SerializeField] private Slider slider;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            slider = GetComponent<Slider>();
        }
        void Update()
        {
            slider.value = player.WaterLevel;
        }
    }
}