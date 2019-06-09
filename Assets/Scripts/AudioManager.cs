using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip music;
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

    private void Start()
    {
        PlayMusic();
    }

    private void PlayMusic()
    {
        source.Play();
    }
}
