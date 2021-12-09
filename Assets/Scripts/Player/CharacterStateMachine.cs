using UnityEngine;
using System.Collections.Generic;

public class CharacterStateMachine
{
    public Inputs currentInput;
    public State currentState;

    private Stack<State> _stateStack;
    private PlayerController _characterController;

    public CharacterStateMachine(PlayerController playerController)
    {
        _characterController = playerController;
        _stateStack = new Stack<State>();
        currentState = State.Idle;
        _stateStack.Push(currentState);
    }

    public Inputs CurrentInput
    {
        set
        {
            if (value != this.currentInput)
            {
                this.currentInput = value;
                OnInputChanged();
            }
        }
    }

    private void OnInputChanged()
    {
        SetNewState(currentInput);
        _characterController.CurrentPlayerState = currentState;
    }

    public void SetNewState(Inputs newInput)
    {
        switch (newInput)
        {
            case Inputs.Move:
                switch (currentState)
                {
                    case State.Idle:
                        currentState = State.Walking;
                        _stateStack.Push(currentState);
                        break;
                    case State.Running:
                        currentState = State.Walking;
                        _stateStack.Push(currentState);
                        break;
                    case State.Crouching:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Ctrl:
                switch (currentState)
                {
                    case State.Jumping:
                        break;
                    case State.Idle:
                        currentState = State.Crouching;
                        _stateStack.Push(currentState);
                        break;
                    case State.Walking:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                    case State.Running:
                        currentState = State.Sneaking;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Space:
                switch (currentState)
                {
                    case State.Crouching: break;
                    case State.Jumping: break;
                    default:
                        currentState = State.Jumping;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Shift:
                switch (currentState)
                {
                    case State.Jumping:
                        break;
                    case State.Walking:
                        currentState = State.Running;
                        _stateStack.Push(currentState);
                        break;
                }

                break;
            case Inputs.Release:
                switch (currentState)
                {
                    case State.Idle:
                        break;
                    default:
                        ReturnToPreviousState();
                        Debug.Log("key  released. returning to previous state: " + currentState);
                        break;
                }

                break;
            case Inputs.ReleaseInAir:
                switch (currentState)
                { 
                    default: 
                        ReturnToPreviousState(); 
                        ReturnToPreviousState(); 
                        Debug.Log("key  released mid air. returning to previous state: " + currentState);
                        break;
                }

                break;
        }
    }

    public void ReturnToPreviousState()
    {
        if (_stateStack.Count > 1)
        {
            _stateStack.Pop();
        }
        
        currentState = _stateStack.Peek();
        _characterController.CurrentPlayerState = currentState;
    }
}