using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private int _stackPointer;
    public static State currentState;
    private State[] _stateStack;

    public enum State
    {
        Idle, 
        Walking,
        Crouching, 
        Sneaking,
        NotGrounded, 
        Running,
    }

    public enum Inputs
    {
        Down,
        Move,
        Space, 
        Shift, 
        Release
    }

    private void Start()
    {
        _stateStack = new State[4];
        currentState = State.Idle;
        Push(currentState);
    }

    public void SetNewState(Inputs newInput)
    {
        switch (newInput)
        {
            case Inputs.Move:
                switch (currentState)
                { 
                    case State.Idle: currentState = State.Walking; break;
                    case State.Crouching: currentState = State.Sneaking; break;
                }
                Push(currentState);
                break;
            case Inputs.Down:
                switch (currentState)
                {
                    case State.Idle: currentState = State.Crouching; break;
                    case State.Running: currentState = State.Sneaking; break;
                }
                Push(currentState);
                break;
            case Inputs.Space:
                switch (currentState)
                {
                    case State.Crouching: break;
                    default: currentState = State.NotGrounded; break;
                }
                Push(currentState);
                break;
            case Inputs.Shift:
                switch (currentState)
                { 
                    case State.Walking: currentState = State.Running; break;
                } 
                Push(currentState);
                break;
            case Inputs.Release:
                switch (currentState)
                {
                    default: currentState = Pop(); break;
                }
                break;
        }
    }

    private void Push(State state)
    {
        _stateStack[_stackPointer] = state;
        _stackPointer++;
    }

    private State Pop()
    {
        _stackPointer--;
        return _stateStack[_stackPointer];
    }
}
