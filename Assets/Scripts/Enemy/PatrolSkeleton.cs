using UnityEngine;
using UnityEngine.AI;

public class PatrolSkeleton : Enemy
{
    Animator anim;  // Detection range for player
    NavMeshAgent agent; // Reference to the NavMeshAgent

    private Vector3 home;
    private float dist;
    private Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        home = this.transform.position;
        isDead = false;
        lastPosition = agent.nextPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            // Distance to the target
            dist = Vector3.Distance(player.transform.position, this.transform.position);

            // If inside the lookRadius
            if (dist <= lookRadius)
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isPatrolling", false);
                if (dist <= agent.stoppingDistance) // if within attack distance, 
                {                                       //target and attack
                    this.FaceTarget();
                    agent.SetDestination(this.transform.position);
                    
                    anim.SetBool("isAttacking", true);
                    anim.SetBool("isRunning", false);
                }
                else
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isAttacking", false);

                    agent.SetDestination(player.transform.position);
                }
            }
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isPatrolling", true);
                
                if(agent.velocity == Vector3.zero && Vector3.Distance(transform.position, agent.nextPosition) <= 4f)
                {
                    anim.SetBool("isIdle", true);
                }
                else
                {
                    anim.SetBool("isIdle", false);
                }

            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
            {
                agent.SetDestination(this.transform.position);
            }
            
        }

        if(currentHealth <= 0)
        {
            die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyProjectile")
        {
            anim.SetBool("isDead", true);
            isDead = true;
        }
    }

    //Event triggered by the Animation "Attack" for the skeleton.
    private void EndAttack()
    {
        if(dist <= agent.stoppingDistance)
        {
            player.takeDamage(atkDamage);
        }
    }

    //Event triggered by animation
    private void IsDead()
    {
        Destroy(this.gameObject);
    }
}

