using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class State : MonoBehaviour
{
    protected StateMachine Machine;

    public static implicit operator bool(State state)
    {
        return state != null;
    }

    public void Initialize(StateMachine machine)
    {
        Machine = machine;
        OnStateInitialize(machine);
    }

    protected virtual void OnStateInitialize(StateMachine machine = null)
    {
    }

    public void StateEnter()
    {
        enabled = true;
        OnStateEnter();
    }

    protected virtual void OnStateEnter()
    {
    }

    public void StateExit()
    {
        OnStateExit();
        enabled = false;
    }

    protected virtual void OnStateExit()
    {
    }

    public void OnEnable()
    {
        if (this != Machine.GetCurrentState)
        {
            enabled = false;
            Debug.LogWarning("Do not enable States directly. Use StateMachine.SetState");
        }
    }

    public void OnDisable()
    {
        if (this == Machine.GetCurrentState)
        {
            enabled = true;
            Debug.LogWarning("Do not disable States directly. Use StateMachine.SetState");
        }
    }
}