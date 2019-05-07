using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class MushController : Enemy
{
    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    GameObject prefab;
    public Transform projectileSpawn;
    public float coolDown = 1.5f;
    public float projectileSpeed = 2700f;
    public float shootRadius = 35f;
    public float enemySpeed = 5f;
    private float distance;

    private float coolDownTimer;
    Animator anim;

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
        // Distance to the target
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            anim.SetBool("isIdle", false);
            FaceTarget();
            // If within melee distance, squash!
            if (distance <= agent.stoppingDistance)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRetreating", true);
                agent.SetDestination(transform.position);
            }
            else    //chase until within shooting distance
            {
                if (distance <= shootRadius)
                {
                    agent.SetDestination(transform.position);
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isRetreating", false);
                    anim.SetBool("isAttacking", true);

                }
                else
                {
                    agent.SetDestination(target.position);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isRetreating", false);
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
            anim.SetBool("isRetreating", false);

        }
    }

    // event called by mush attack01 animation
    private void IsShooting()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(prefab, projectileSpawn.position, projectileSpawn.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        Vector3 direction = (target.position - projectileSpawn.position).normalized;
        rb.AddForce(direction * projectileSpeed);
    }


    //if player is in distance, deal damage
    private void IsSquashing()
    {
        if (distance <= agent.stoppingDistance)
        {
            player.takeDamage(atkDamage);
        }
    }
    
}