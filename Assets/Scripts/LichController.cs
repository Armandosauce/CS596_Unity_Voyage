using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LichController : Enemy
{


    public Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    GameObject prefab;
    Animator anim;
    public Transform projectileSpawn;

    public float coolDown = 1.5f;
    public float enemySpeed = 0.1f;
    public float projectileSpeed = 2700f;
    public float shootRadius = 50f;

    private float coolDownTimer;
    private bool shoot;

    public Transform ProjectileSpawn
    {
        get
        {
            return projectileSpawn;
        }

        set
        {
            projectileSpawn = value;
        }
    }

    void Start()
    {
        prefab = Resources.Load("projectile") as GameObject;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        coolDownTimer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Time.deltaTime;
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            anim.SetBool("isIdle", false);
            // If within melee distance, run!
            if (distance <= agent.stoppingDistance)
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", true);
                RunAway();
            }
            else    //chase until within shooting distance
            {
                if (distance <= shootRadius)
                {
                    agent.SetDestination(transform.position);
                    FaceTarget();
                    if (coolDownTimer <= 0)
                    {
                        anim.SetBool("isAttacking", true);
                        anim.SetBool("isRunning", false);
                    }
                }
                else
                {
                    FaceTarget();
                    agent.SetDestination(target.position);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRunning", true);
                }
            }
        }
        //idle
        else
        {
            agent.SetDestination(transform.position);
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);

        }

        if(currentHealth <= 0)
        {
            die();
        }
    }


    void RunAway()
    {
        Vector3 direction = -((target.position - transform.position).normalized);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.Translate(0, 0, enemySpeed);
        coolDownTimer = coolDown;
    }
    
    // launch projectile, called by event on attack01 animation
    private void IsShooting()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(prefab, projectileSpawn.position, projectileSpawn.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        Vector3 direction = (target.position - projectileInstance.transform.position).normalized;
        rb.AddForce(direction * projectileSpeed);
    }

    //event on animation
    private void IsDead()
    {
        Destroy(this.gameObject);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
