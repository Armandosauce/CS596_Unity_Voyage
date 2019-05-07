using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [HideInInspector]
    public AudioSource source;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        source = GetComponent<AudioSource>();
    }
}
