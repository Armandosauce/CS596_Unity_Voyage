﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public Vector3 MoveInput;
    public Vector2 MouseInput;
    public bool JumpInput;
}

public class PlayerInputController : MonoBehaviour {

    public PlayerInput Current;
    private Vector3 moveInput;
    private Vector2 mouseInput;
    private bool jumpInput;

	// Use this for initialization
	void Start () {
        Current = new PlayerInput();		
	}

    // Update is called once per frame
    void Update() {

        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        bool jumpInput = Input.GetButtonDown("Jump");

        Current = new PlayerInput()
        {
            MoveInput = moveInput,
            MouseInput = mouseInput,
            JumpInput = jumpInput
        };
	}
}
