using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpItemsController : MonoBehaviour
{
    public AudioClip itemCollectedSound;
    
    public int healthPickup;
    public float spinSpeed = 100f;
    
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1), spinSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.source.PlayOneShot(itemCollectedSound, 1f);
            
            if (PlayerManager.instance.player.currentHealth < PlayerManager.instance.player.startHealth)
            {
                // Generate a random value for the health pack and make sure the player doesn't go above max health
                healthPickup = Random.Range(5, 20);
                if((PlayerManager.instance.player.currentHealth + healthPickup) <= PlayerManager.instance.player.startHealth)
                {
                    PlayerManager.instance.player.currentHealth += healthPickup;
                    Debug.Log("Health restored: " + healthPickup);
                }
                else
                {
                    PlayerManager.instance.player.currentHealth = PlayerManager.instance.player.startHealth;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
