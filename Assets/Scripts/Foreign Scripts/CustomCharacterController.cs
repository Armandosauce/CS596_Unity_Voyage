using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour {

    private float radius;

    private bool contact;

    private void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }
	
	// Update is called once per frame
	void Update () {

        contact = false;

        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            Vector3 contactPoint = Vector3.zero;

            if(col is BoxCollider)
            {
                contactPoint = ClosestPointOn((BoxCollider)col, transform.position);
            }
            else if (col is SphereCollider)
            {
                contactPoint = ClosestPointOn((SphereCollider)col, this.transform.position);
            }

            DebugDraw.DrawMarker(contactPoint, 2.0f, Color.red, 0.0f, false);

            Vector3 v = transform.position - contactPoint;

            transform.position += Vector3.ClampMagnitude(v, Mathf.Clamp(radius - v.magnitude, 0, radius));

            contact = true;
        }
    }

    Vector3 ClosestPointOn(BoxCollider collider, Vector3 to)
    {
        //if the box in world space has an identity rotation avoid calling Rot function
        if (collider.transform.rotation == Quaternion.identity)
        {
            return collider.ClosestPointOnBounds(to);
        }

        return closestPointOnRot(collider, to);
    }
    
    //for player collisions with Sphere objects
    Vector3 ClosestPointOn(SphereCollider collider, Vector3 to)
    {
        Vector3 p;

        p = to - collider.transform.position;
        p.Normalize();

        p *= collider.radius * collider.transform.localScale.x;
        p += collider.transform.position;

        return p;
    }

    //For player collisions on boxes that have a non-identity rotation on it
    Vector3 closestPointOnRot(BoxCollider collider, Vector3 to)
    {
        // Cache the collider transform
        var ct = collider.transform;

        // Firstly, transform the point into the space of the collider
        var local = ct.InverseTransformPoint(to);

        // Now, shift it to be in the center of the box
        local -= collider.center;

        // Inverse scale it by the colliders scale
        var localNorm =
        new Vector3(
        Mathf.Clamp(local.x, -collider.size.x * 0.5f, collider.size.x * 0.5f),
        Mathf.Clamp(local.y, -collider.size.y * 0.5f, collider.size.y * 0.5f),
        Mathf.Clamp(local.z, -collider.size.z * 0.5f, collider.size.z * 0.5f)
        );

        // Now we undo our transformations
        localNorm += collider.center;

        // Return resulting point
        return ct.TransformPoint(localNorm);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = contact ? Color.cyan : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

