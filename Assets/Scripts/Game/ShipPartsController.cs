using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartsController : MonoBehaviour
{
    public bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PlayerCharacter")
        {
            collected = true;
            transform.gameObject.SetActive(false);
            Debug.Log("Missing part collected.");
        }
    }
}
