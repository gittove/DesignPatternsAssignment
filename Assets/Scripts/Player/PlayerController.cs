using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State currentState;
    
    [SerializeField] [Range(0, 10)] private float _walkAcceleration;
    [SerializeField] [Range(0, 10)] private float _runningAcceleration;
    [SerializeField] [Range(0, 10)] private float _sneakAcceleration;
    [SerializeField] [Range(0, 10)] private float _jumpAcceleration;
    
    [SerializeField] [Range(0, 20)] private float _maxWalkSpeed;
    [SerializeField] [Range(0, 20)] private float _maxRunningSpeed;
    [SerializeField] [Range(0, 20)] private float _maxSneakingSpeed;
    [SerializeField] [Range(0, 20)] private float _maxJumpHeight;
    
    private CharacterStateMachine _stateMachine;
    private CharacterMovements _characterMovements;
    private PlayerInputs _playerInputs;
    private Rigidbody _rbody;
    private float[] movementValues;

    void Awake()
    {
        movementValues = new float[8]
        {
            _walkAcceleration, _maxWalkSpeed, _runningAcceleration, _maxRunningSpeed, _sneakAcceleration,
            _maxSneakingSpeed, _jumpAcceleration, _maxJumpHeight
        };
        
        _rbody = GetComponent<Rigidbody>();
        _stateMachine = new CharacterStateMachine(this);
        _playerInputs = new PlayerInputs();
        _characterMovements = new CharacterMovements(_rbody, movementValues);
    }

    private void Update()
    {
        _stateMachine.CurrentInput = _playerInputs.GetKeys(_stateMachine.currentInput);
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Walking: _characterMovements.Walk();
                break;
            case State.Running: _characterMovements.Run();
                break;
            case State.Sneaking: _characterMovements.Sneak();
                break;
        }
        Debug.Log("Current state: " + currentState);
    }
}
