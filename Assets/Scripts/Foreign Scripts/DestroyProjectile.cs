using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

    public Player player;
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag != "Enemy")
            Destroy(this.gameObject);
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInParent<Player>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
