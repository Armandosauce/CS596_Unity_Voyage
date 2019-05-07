using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {
    
    public float damage;
    public float activeTime;

    private void Start()
    {
        StartCoroutine(timedDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Enemy")
            Destroy(this.gameObject);
        if (collision.gameObject.tag == "PlayerHitbox")
        {
            PlayerManager.instance.player.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    //Destroys the projectile after a set amount of time
    //This keeps the Projectile objects in the scene below 
    //a fixed amount at any given time
    private IEnumerator timedDestroy()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }
}
