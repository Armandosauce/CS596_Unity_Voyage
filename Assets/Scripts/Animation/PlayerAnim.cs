using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnim : MonoBehaviour {

    public Animator anim;
    public float animMultiplier;

    private PlayerInputController input;
    private PlayerMotor player;
    private float animDefaultSpeed;
    
    private void Start()
    {
        input = GetComponentInParent<PlayerInputController>();
        
        animDefaultSpeed = anim.speed;

    }

    // Update is called once per frame
    void Update () {
    
        if (input.Current.RunInput)
        {
            anim.speed = animMultiplier * animDefaultSpeed;
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

        anim.SetFloat("x_move", input.Current.MoveInput.x);
        anim.SetFloat("y_move", input.Current.MoveInput.z);
    }

    
}
