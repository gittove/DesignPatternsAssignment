using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _currentDirection;
    private float _walkAcceleration = 5f;
    private float _runningAcceleration = 8f;
    private float _sneakAcceleration = 3f;
    private float _jumpAcceleration = 4f;
    private float _maxWalkSpeed = 15;
    private float _maxRunningSpeed = 20;
    private float _maxSneakingSpeed = 8;
    private float _maxJumpHeight = 10f;
    private Vector3 _currentVelocity;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // GroundCheck
    }

    private void FixedUpdate()
    {
        if (PlayerStateMachine.currentState == PlayerStateMachine.State.Walking)
        {
            Walk();
        }

        if (PlayerStateMachine.currentState == PlayerStateMachine.State.Running)
        {
            Run();
        }

        if (PlayerStateMachine.currentState == PlayerStateMachine.State.Sneaking)
        {
            Sneak();
        }
    }

    private void Walk()
    {
        _currentDirection.x = Input.GetAxisRaw("Horizontal");
        _currentDirection.z = Input.GetAxisRaw("Vertical");
        _currentVelocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0, _maxWalkSpeed), 0, Mathf.Clamp(_rb.velocity.z, 0, _maxWalkSpeed));

        if (_currentVelocity.x < _maxWalkSpeed || _currentVelocity.z < _maxWalkSpeed)
        {
            _currentVelocity.x += _walkAcceleration * _currentDirection.x;
            _currentVelocity.z += _walkAcceleration * _currentDirection.z;
        }

        _rb.velocity = _currentVelocity;
    }

    private void Run()
    {
        _currentDirection.x = Input.GetAxisRaw("Horizontal");
        _currentDirection.z = Input.GetAxisRaw("Vertical");
        _currentVelocity = new Vector3(_rb.velocity.x, _rb.velocity.z, 0);
        
        if (_currentVelocity.x < _maxRunningSpeed || _currentVelocity.z < _maxRunningSpeed)
        {
            _currentVelocity.x += _runningAcceleration * _currentDirection.x;
            _currentVelocity.z += _runningAcceleration * _currentDirection.z;
        }

        _rb.velocity = _currentVelocity;
    }

    private void Sneak()
    {
        _currentDirection.x = Input.GetAxisRaw("Horizontal");
        _currentDirection.z = Input.GetAxisRaw("Vertical");
        _currentVelocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0, _maxSneakingSpeed), 0, Mathf.Clamp(_rb.velocity.z, 0, _maxSneakingSpeed));

        if (_currentVelocity.x < _maxSneakingSpeed || _currentVelocity.z < _maxRunningSpeed)
        {
            _currentVelocity.x += _sneakAcceleration * _currentDirection.x;
            _currentVelocity.z += _sneakAcceleration * _currentDirection.z;
        }

        _rb.velocity = _currentVelocity;
    }
}
