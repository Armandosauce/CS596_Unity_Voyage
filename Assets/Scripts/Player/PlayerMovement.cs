using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Declarations
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    public float groundDist;
    public LayerMask ground;
    public Vector3 drag;
    public float jumpPressedRememberTime;
    public float groundedRememberTime;

    private CharacterController _controller;
    private Vector3 _velocity;
    private PlayerInputController input;
    private Vector3 previousPos;
    private bool _isGrounded;
    private Transform _groundCheck;
    private float moveSpeed;
    private float jumpPressedRemember;
    private float groundedRemember;
    #endregion

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputController>();
        _groundCheck = transform.GetChild(0);
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDist, ground, QueryTriggerInteraction.Ignore);

        /*
        #region Jump
        
        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;

        if (_isGrounded )
        {
            groundedRemember = groundedRememberTime;
        }
        if (input.Current.JumpInput)
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        Debug.Log(groundedRemember.ToString() + ", " + jumpPressedRemember.ToString());
        if ((groundedRemember > 0) && (jumpPressedRemember > 0)) {
            _velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            groundedRemember = 0;
            jumpPressedRemember = 0;
        }

        #endregion
        
        */

        if (!_isGrounded)
        {
            _velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0;
        }

        if (input.Current.JumpInput && _isGrounded) {
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

        if (input.Current.MoveInput != Vector3.zero)
            transform.forward = input.Current.MoveInput;

        _velocity.x /= 1 + drag.x * Time.deltaTime;
        _velocity.y /= 1 + drag.y * Time.deltaTime;
        _velocity.z /= 1 + drag.z * Time.deltaTime;

        _velocity.x = input.Current.MoveInput.x * moveSpeed;
        _velocity.z = input.Current.MoveInput.z * moveSpeed;
        
        _controller.Move(_velocity * Time.deltaTime);
        
       // orientPlayer();
    }

    public Vector3 getVelocity()
    {
        return _controller.velocity;
    }

    public bool isGrounded()
    {
        return _isGrounded;
    }

    private void orientPlayer()
    {
        RaycastHit dr;
        Vector3 upDir;

        Physics.Raycast(this.transform.position + Vector3.up, Vector3.down, out dr);

        upDir = Vector3.Cross(dr.point - Vector3.up, dr.point - Vector3.up).normalized;

        Debug.DrawRay(dr.point, Vector3.up);
        transform.up = upDir;
    }
}