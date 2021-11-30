using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private PlayerStateMachine _psm;

    private void Awake()
    {
        _psm = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            _psm.SetNewState(PlayerStateMachine.Inputs.Move);
        }

        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _psm.SetNewState(PlayerStateMachine.Inputs.Down);
        }
        
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _psm.SetNewState(PlayerStateMachine.Inputs.Shift);
        }
        
        // todo GetKeyUp => Input.Release
    }
}
