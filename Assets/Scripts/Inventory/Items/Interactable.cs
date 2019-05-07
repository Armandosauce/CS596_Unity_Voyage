using UnityEngine;

public class Interactable : MonoBehaviour
{

    public float radius;
    public float interactRadius;

    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    public Transform interactionTransform;

    public Transform player;

    public virtual void Interact()
    {
        //This method will be overwritten
        Debug.Log("Interacting with: " + interactionTransform.name);

    }
    
    void Update()
    {
        float distance = Vector2.Distance(player.position, interactionTransform.position);

        if (distance <= interactRadius)
        {
            Interact();
        }

        if (distance <= radius)
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, smoothTime);

        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

    }

}
