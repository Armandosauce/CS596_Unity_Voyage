using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    public float maxHealth;
    
    // time which an enemy cannot be hit again (is invincible)
    [SerializeField]
    protected float inv_time;
    [SerializeField]
    protected Player player;
    [SerializeField]
    protected AudioClip awakeSound;
    [SerializeField]
    protected AudioClip atkSound;

    [SerializeField]
    protected float lookRadius;

    [SerializeField]
    protected float runningSpeed;
    [SerializeField]
    protected float attackingSpeed;
    [SerializeField]
    protected float atkDamage;
    protected bool isDead;

    public bool isVulnerable { get; private set; }
    public float currentHealth { get; private set; }

    private void Awake()
    {
        currentHealth = maxHealth;        
    }
    public void takeDamage(float x)
    {
        currentHealth -= x;
        Debug.Log(this.name + " health:" + currentHealth);
        StartCoroutine(invulnerable());
    }
    
    private IEnumerator invulnerable()
    {
        isVulnerable = false;
        yield return new WaitForSeconds(inv_time);
        isVulnerable = true;
    }
    
    // Rotate to face the target
    protected void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    protected void die()
    {
        Destroy(this.gameObject);
    }

    public void PlayAwakeSound( float volume)
    {
        AudioManager.instance.source.PlayOneShot(awakeSound, volume);
    }

    public void PlayAttackSound(float volume)
    {
        AudioManager.instance.source.PlayOneShot(atkSound, volume);
    }

}