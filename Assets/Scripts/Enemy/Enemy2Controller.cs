using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class Enemy2Controller : MonoBehaviour
{

    public float lookRadius = 100f;  // Detection range for player

    Transform target;   // Reference to the player
    NavMeshAgent agent; // Reference to the NavMeshAgent
    GameObject prefab;
    public Transform projectileSpawn;
    float coolDown = 1.5f;
    public float projectileSpeed = 2700f;

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

        // If inside the lookRadius, face and shoot
        if (distance <= lookRadius)
        {
            coolDown -= Time.deltaTime;
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            if (coolDown <= 0)
            {
                ShootTarget();
                coolDown = 2f;
            }
        }
    }

    // Rotate to face the target
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