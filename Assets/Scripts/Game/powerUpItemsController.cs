using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpItemsController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip itemCollectedSound;
    GameObject player;
    public float healthPickup;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        player = GameObject.Find("PlayerCharacter");
        healthPickup = 10;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerCharacter")
        {
            audioSource.PlayOneShot(itemCollectedSound, 1f);
            if (player.gameObject.GetComponent<Player>().currentHealth < player.gameObject.GetComponent<Player>().health)
            {
                player.gameObject.GetComponent<Player>().currentHealth += healthPickup;
            }
            Destroy(this.gameObject);
            Debug.Log("Health pack collected.");
        }
    }
}
