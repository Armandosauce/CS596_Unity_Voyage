using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    public Transform dest;
    public float lookRadius = 10f;


    Transform target;   // Reference to the player
    bool onGround; // 

    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        onGround = true;
           
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (onGround)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().useGravity = false;
                    this.transform.position = dest.position;
                    this.transform.parent = GameObject.Find("ItemPosition").transform;
                    onGround = false;
                }
                else
                {
                    this.transform.parent = null;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<BoxCollider>().enabled = true;
                    onGround = true;
                }
            }

        }
    }

    /*
    private void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = dest.position;
        this.transform.parent = GameObject.Find("ItemPosition").transform;
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    */

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
