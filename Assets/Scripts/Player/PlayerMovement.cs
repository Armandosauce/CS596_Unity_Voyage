using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    public Vector3 drag;

    private CharacterController _controller;
    private Vector3 _velocity;
    private PlayerInputController input;
    private Vector3 previousPos;
    private float moveSpeed;
    
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputController>();
    }

    void Update()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -4f;
            if (input.Current.JumpInput)
                _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (input.Current.RunInput)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        _controller.Move(input.Current.MoveInput * Time.deltaTime * moveSpeed);
        if (input.Current.MoveInput != Vector3.zero)
            transform.forward = input.Current.MoveInput;
        
        _velocity.y += gravity * Time.deltaTime;

        _velocity.x /= 1 + drag.x * Time.deltaTime;
        _velocity.y /= 1 + drag.y * Time.deltaTime;
        _velocity.z /= 1 + drag.z * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public Vector3 getVelocity()
    {
        return _controller.velocity;
    }

}