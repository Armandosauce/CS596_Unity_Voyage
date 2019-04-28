using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EvasiveController : MonoBehaviour { 

    public float lookRadius = 100f;  // Detection range for player

    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    GameObject prefab;
    public Transform projectileSpawn;

    public float coolDown = 1.5f;
    public float enemySpeed = 0.1f;
    public float projectileSpeed = 2700f;
    public float shootRadius = 50f;
    bool shoot;


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
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            // If within melee distance, run!
            if (distance <= agent.stoppingDistance)
            {
                RunAway();
                coolDown = 1f;
            }
            else    //chase until within shooting distance
            {
                if (distance <= shootRadius)
                {
                    coolDown -= Time.deltaTime;
                    shoot = true;
                    FaceTarget();
                    if (coolDown <= 0)
                    {
                        ShootTarget();
                        coolDown = 2f;
                    }
                }
                else
                {
                    shoot = false;
                    FaceTarget();
                }
            }
        }
        //idle
        else agent.SetDestination(transform.position);
    }


    void RunAway()
    {
        Vector3 direction = -((target.position - transform.position).normalized);
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        transform.Translate(0, 0, enemySpeed);
    }

    //false to chase, true to stand and shoot
    void FaceTarget()
    { 
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (!shoot) agent.SetDestination(target.position);
        else agent.SetDestination(transform.position);
    }

    // launch projectile
    void ShootTarget()
    {
        GameObject projectileInstance;
        projectileInstance = Instantiate(prefab, projectileSpawn.position, projectileSpawn.rotation);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        rb.AddForce(projectileSpawn.forward * projectileSpeed);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
