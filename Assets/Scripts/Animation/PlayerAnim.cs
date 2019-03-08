using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInputController))]
public class PlayerAnim : MonoBehaviour {

    public Animator anim;

    private PlayerInputController input;
    private PlayerMovement player;
    private float animDefaultSpeed;
    
    private void Start()
    {
        input = GetComponent<PlayerInputController>();
        player = GetComponent<PlayerMovement>();
        animDefaultSpeed = anim.speed;

    }

    // Update is called once per frame
    void Update () {
    
        if (input.Current.RunInput)
        {
            anim.speed = 2 * animDefaultSpeed;
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
        
	}

    
}
