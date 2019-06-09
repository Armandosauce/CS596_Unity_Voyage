using UnityEngine;
using UnityEngine.AI;

public class SkeletonContoller : Enemy
{
    Animator anim;  // Detection range for player
    NavMeshAgent agent; // Reference to the NavMeshAgent

    private Vector3 home;
    private float dist;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        home = this.transform.position;
        isDead = false;
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
                if(Mathf.Abs(Vector3.Distance(this.transform.position, home)) <= agent.stoppingDistance)
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isRunning", false);
                    agent.SetDestination(this.transform.position);

                }
                else
                {
                    agent.SetDestination(home);
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

