using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsController : MonoBehaviour
{
    public bool collected = false;
    
    public AudioClip itemCollectedSound;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collectPart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collectPart();
        }
    }
    
    private void collectPart()
    {
        AudioManager.instance.source.PlayOneShot(itemCollectedSound, 1f);

        collected = true;
        transform.gameObject.SetActive(false);
        Debug.Log("Missing part collected.");
    }
}
