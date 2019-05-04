using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamagePlayer : MonoBehaviour
{
    Animator anim;
    public float atkDamage = 15f;
    public Player target;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && anim.GetBool("isAttacking"))
        {
            target.takeDamage(atkDamage);
        }
    }
}
