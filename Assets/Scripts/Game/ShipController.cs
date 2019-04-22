using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public int missingParts = 1;
    public bool isComplete = false;

    public GameObject missingPart;

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
        // Consider making it a trigger instead of using RigidBody for collision
        if (collision.gameObject.name == "PlayerCharacter")
        {
            if (missingPart.GetComponent<ShipPartsController>().collected == true)
            {
                missingParts--;
                Destroy(missingPart);
                Debug.Log("Missing part delivered.");
            }

            if (missingParts == 0)
            {
                isComplete = true;
                Debug.Log("Ship is complete!");
            }
        }
    }
}
