using UnityEngine;

public struct PlayerInputHandler
{
    public Inputs GetKeys(Inputs currentInput, State currentState)
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            return Inputs.Release;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            return Inputs.Ctrl;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return Inputs.Space;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            return Inputs.Shift;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return Inputs.Move;
        }

        if (!Input.GetKey(KeyCode.W) && 
            !Input.GetKey(KeyCode.A) && 
            !Input.GetKey(KeyCode.S) && 
            !Input.GetKey(KeyCode.D) 
            && currentState != State.Jumping)
        {
            return Inputs.Release;
        }

        return currentInput;
    }

    public Inputs GetInputAfterLanding(Inputs currentInput)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return Inputs.Ctrl;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return Inputs.Move;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return Inputs.Shift;
        }
        
        return currentInput;
    }

    public bool GetClick()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            return false;
        }

        return false;
    }
}