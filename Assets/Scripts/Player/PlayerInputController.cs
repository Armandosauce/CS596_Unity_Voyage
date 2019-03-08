using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
    public bool RunInput;
}

public class PlayerInputController : MonoBehaviour {

    public PlayerInput Current;
    private Vector3 moveInput;
    private Vector2 mouseInput;
    private bool jumpInput;
    private bool runInput;

	// Use this for initialization
	void Start () {
        Current = new PlayerInput();		
	}

    // Update is called once per frame
    void Update() {

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        jumpInput = Input.GetButtonDown("Jump");

        runInput = Input.GetButton("Run");

        Current = new PlayerInput()
        {
            MoveInput = moveInput,
            MouseInput = mouseInput,
            JumpInput = jumpInput,
            RunInput = runInput
        };
	}
}
