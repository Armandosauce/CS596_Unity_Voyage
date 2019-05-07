using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpItemsController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip itemCollectedSound;
    GameObject player;
    public int healthPickup;
    public float spinSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        player = GameObject.Find("PlayerCharacter");
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,0,1), spinSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(itemCollectedSound, 1f);
            float currentHealth = player.gameObject.GetComponent<Player>().currentHealth;
            float maxHealth = player.gameObject.GetComponent<Player>().startHealth;

            if (currentHealth < maxHealth)
            {
                // Generate a random value for the health pack and make sure the player doesn't go above max health
                healthPickup = Random.Range(5, 20);
                if((currentHealth + healthPickup) <= maxHealth)
                {
                    player.gameObject.GetComponent<Player>().currentHealth += healthPickup;
                    Debug.Log("Health restored: " + healthPickup);
                }
                else
                {
                    player.gameObject.GetComponent<Player>().currentHealth = maxHealth;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
