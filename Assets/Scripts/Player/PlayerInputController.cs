using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
    public bool RunInput;
    public bool InventoryInput;
    public bool Fire;
}

public class PlayerInputController : MonoBehaviour {

    public PlayerInput Current;
    private Vector3 moveInput;
    private Vector2 mouseInput;
    private bool jumpInput;
    private bool runInput;
    private bool inventoryInput;
    private bool fire;
    private bool pause;

	// Use this for initialization
	void Start () {
        Current = new PlayerInput();
        pause = false;
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetButtonDown("Pause"))
        {
            pause = !pause;
            GameManager.instance.Pause(pause);
        }

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        jumpInput = Input.GetButtonDown("Jump");

        runInput = Input.GetButton("Run");

        inventoryInput = Input.GetButtonDown("Inventory");
        
        fire = Input.GetButtonDown("Fire1");
        
        Current = new PlayerInput()
        {
            MoveInput = moveInput,
            MouseInput = mouseInput,
            JumpInput = jumpInput,
            RunInput = runInput,
            InventoryInput = inventoryInput,
            Fire = fire
        };
	}
}
