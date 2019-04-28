using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    public float jumpPressedRememberTime;
    public float jumpForce;
    public float gravity;
    public float moveSpeed;
    public float fallMultiplier;
    public float fallThreshold;
    public Camera cam;
    public Vector3 rotationOffset;
    private CharacterController _controller;
    private float verticalVelocity;
    private PlayerInputController input;
    private Vector3 playerMovement;
    private Vector3 direction;
    
    public float groundedRememberTime;
    public LayerMask ground;
    public float groundDist;
    private Transform _groundCheck;
    private bool _isGrounded;
    private float jumpPressedRemember;
    private float groundedRemember;
    private float groundIndex;
    RaycastHit hit;
    Ray down;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInputController>();
        _groundCheck = transform.GetChild(0);
        playerMovement = Vector3.zero;
        direction = Vector3.zero;
        verticalVelocity = 0;
        groundedRemember = 0;
        jumpPressedRemember = 0;
        groundIndex = Mathf.Log(ground.value, 2);
    }

    // Update is called once per frame
    void Update()
    {
        handleVertical();

        down = new Ray(transform.position, Vector3.down);
        if (input.Current.MoveInput != Vector3.zero && Physics.Raycast(down, out hit))
        {
            // This if checks if there is something between the ground and the player, 
            // if there is not, then the player will rotate normal to the surface
            Debug.Log("Ground index: " + groundIndex + ", collider index:  " + hit.collider.gameObject.layer);

            if (hit.collider.gameObject.layer == groundIndex)
            {
                transform.forward = Vector3.ProjectOnPlane(cam.transform.forward, hit.normal);
                transform.rotation *= Quaternion.Euler(rotationOffset);
            }

        }

        handleHorizontal();
        playerMovement *= moveSpeed * Time.deltaTime;
        //vertical movement
        playerMovement.y = verticalVelocity * Time.deltaTime;

        _controller.Move(playerMovement);

        //Debug.Log(_isGrounded + ", " + playerMovement + "," + verticalVelocity);
    }


    private void handleHorizontal()
    {
        //horizontal movement

        if (input.Current.MoveInput.x > 0)
        {
            playerMovement.x = cam.transform.right.x;
            playerMovement.z = cam.transform.right.z;
        }
        else if (input.Current.MoveInput.x < 0)
        {
            playerMovement.x = -cam.transform.right.x;
            playerMovement.z = -cam.transform.right.z;
        }


        if (input.Current.MoveInput.z > 0)
        {
            playerMovement.x = cam.transform.forward.x;
            playerMovement.z = cam.transform.forward.z;
        }
        else if (input.Current.MoveInput.z < 0)
        {
            playerMovement.x = -cam.transform.forward.x;
            playerMovement.z = -cam.transform.forward.z;
        }

    }

    private void handleVertical()
    {
        groundedRemember -= Time.deltaTime;
        jumpPressedRemember -= Time.deltaTime;
        _isGrounded = Physics.CheckSphere(_groundCheck.position, groundDist, ground, QueryTriggerInteraction.Ignore);

        if (_isGrounded)
        {
            groundedRemember = groundedRememberTime;
        }
        if (input.Current.JumpInput)
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        //handle falling physics, gets the player to accelerate faster after reaching a certain height
        //jumps feel less "floaty"
        if (!_isGrounded)
        {
            if (verticalVelocity >= fallThreshold)
            {
                verticalVelocity += gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity += gravity * fallMultiplier * Time.deltaTime;
            }
        }
        else
        {
            verticalVelocity = gravity * Time.deltaTime;

        }

        // Jump function, if player was grounded within last 0.X seconds and pressed jump within last 0.X seconds, add jumpForce
        if ((jumpPressedRemember > 0) && (groundedRemember > 0))
        {
            verticalVelocity = jumpForce;
        }

    }
}
