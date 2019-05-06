using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsController : MonoBehaviour
{
    public bool collected = false;

    AudioSource audioSource;
    public AudioClip itemCollectedSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PlayerCharacter")
        {
            audioSource.PlayOneShot(itemCollectedSound, 1f);

            collected = true;
            transform.gameObject.SetActive(false);
            Debug.Log("Missing part collected.");
        }
    }
}
