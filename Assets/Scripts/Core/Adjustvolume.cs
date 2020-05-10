using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjustvolume : MonoBehaviour
{
    float masterVolume = .5f;
    void Start()
    {
        AudioSource[] sources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.volume = masterVolume;
        }
    }
}
