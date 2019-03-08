using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputController))]
public class PlayerAnim : MonoBehaviour {

    public Animator anim;

    private PlayerInputController input;
    private PlayerMovement move;
    private float animDefaultSpeed;

    private void Start()
    {
        input = GetComponent<PlayerInputController>();
        move = GetComponent<PlayerMovement>();
        animDefaultSpeed = anim.speed;
    }

    // Update is called once per frame
    void Update () {

        if(input.Current.RunInput)
        {
            anim.speed *= 3;
        }
        else
        {
            anim.speed = animDefaultSpeed;
        }



        if (input.Current.MoveInput != Vector3.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            
        }

        Debug.Log(move.getVelocity().y);
        if(move.getVelocity().y <= move.gravity / 4 )
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }

	}
}
