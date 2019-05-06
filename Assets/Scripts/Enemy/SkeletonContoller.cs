using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonContoller : MonoBehaviour
{
    Animator anim;
    public float lookRadius = 30f;  // Detection range for player

    public Player target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    public float runningSpeed = 18f;
    public float attackingSpeed = 1f;
    public float atkDamage = 15f;
    public float timer;
    private float dist;
    private bool dead;
  

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        dead = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            // Distance to the target
            dist = Vector3.Distance(target.transform.position, this.transform.position);

            // If inside the lookRadius
            if (dist <= lookRadius)
            {
                anim.SetBool("isIdle", false);
                if (dist <= agent.stoppingDistance) // if within attack distance, 
                {                                       //target and attack
                    FaceTarget();
                    agent.speed = attackingSpeed;
                    agent.SetDestination(this.transform.position);
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    agent.speed = runningSpeed;
                    agent.acceleration = runningSpeed;                //else chase them

                    anim.SetBool("isRunning", true);
                    anim.SetBool("isAttacking", false);

                    agent.SetDestination(target.transform.position);
                }
            }
            else
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);
                agent.SetDestination(this.transform.position);
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
            {
                agent.SetDestination(this.transform.position);
            }
            
        }
    }
    


    // Rotate to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyProjectile")
        {
            anim.SetBool("isDead", true);
            dead = true;
        }
     
    }

    /*

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && anim.GetBool("isAttacking"))
        {
            target.takeDamage(atkDamage);
        }
    }
    */

//Event triggered by the Animation "Attack" for the skeleton.
    private void EndAttack()
    {
        if(dist <= agent.stoppingDistance)
        {
            target.takeDamage(atkDamage);
        }
    }

    //Event triggered by animation

    private void IsDead()
    {
        Destroy(this.gameObject);
    }
}

