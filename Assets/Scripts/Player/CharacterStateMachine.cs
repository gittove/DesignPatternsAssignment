using UnityEngine;
using System.Collections.Generic;

public class CharacterStateMachine
{
    public Inputs currentInput;
    public State currentState;
    
    private Stack <State> _stateStack;
    private PlayerController _characterController;

    public CharacterStateMachine(PlayerController playerController)
    {
        _characterController = playerController;
        _stateStack = new Stack<State>();
        currentState = State.Idle;
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
        _stateStack.Push(currentState);
        SetNewState(currentInput);
        _characterController.currentPlayerState = currentState;
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
                        break;
                    case State.Crouching:
                        currentState = State.Sneaking;
                        break;
                }

                break;
            case Inputs.Ctrl:
                switch (currentState)
                {
                    case State.Idle:
                        currentState = State.Crouching;
                        break;
                    case State.Walking:
                        currentState = State.Sneaking;
                        break;
                    case State.Running:
                        currentState = State.Sneaking;
                        break;
                }

                break;
            case Inputs.Space:
                switch (currentState)
                {
                    case State.Crouching: break;
                    default:
                        currentState = State.NotGrounded;
                        break;
                }

                break;
            case Inputs.Shift:
                switch (currentState)
                {
                    case State.Walking:
                        currentState = State.Running;
                        break;
                }

                break;
            case Inputs.Release:
                switch (currentState)
                {
                    case State.Idle:
                        break;
                    default:
                        currentState = _stateStack.Pop();
                        break;
                }
                break;
        }
    }
}