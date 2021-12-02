using UnityEngine;

public struct PlayerInputs
{
    public Inputs GetKeys(Inputs currentInput)
    {
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W)
            || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            return Inputs.Release;
        }
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return Inputs.Ctrl;
        }
        
        // todo space => jump
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return Inputs.Shift;
        }
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return Inputs.Move;
        }

        return currentInput;
    }
}


