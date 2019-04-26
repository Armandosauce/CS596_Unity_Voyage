using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonContoller : MonoBehaviour
{
    Animator anim;
    public float lookRadius = 30f;  // Detection range for player

    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            anim.SetBool("isIdle", false);
            if (distance <= agent.stoppingDistance) // if within attack distance, 
            {                                       //target and attack
                FaceTarget();
                agent.speed = 2f;
                agent.acceleration = 2f;
                anim.SetBool("isAttacking", true);
                anim.SetBool("isRunning", false);
            }
            else
            {
                agent.speed = 18f;
                agent.acceleration = 18f;                //else chase them
                anim.SetBool("isRunning", true);
                anim.SetBool("isAttacking", false);
                agent.SetDestination(target.position);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
        }
    }


    // Rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

