using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupItem : MonoBehaviour
{
    public Transform dest;
    public float lookRadius = 10f;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider col;
    [SerializeField]
    private Transform itemPosition;
    float distance;

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
        distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (onGround)
                {
                    col.enabled = false;
                    rb.freezeRotation = true;
                    rb.useGravity = false;
                    this.transform.position = dest.position;
                    this.transform.parent = itemPosition.transform;
                    onGround = false;
                }
                else
                {
                    this.transform.parent = null;
                    rb.useGravity = true;
                    col.enabled = true;
                    onGround = true;
                }
            }

        }
    }

    /*
     * Enable for mouse click-hold to pickup
     * 
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

    private void OnGUI()
    {
        if (distance <= lookRadius && onGround)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "'E' to pickup");
        }
        if (distance <= lookRadius && !onGround)
        {
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "'E' to drop");
        }
    }

}
