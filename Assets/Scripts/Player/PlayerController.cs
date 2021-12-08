using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public State currentPlayerState;

    [SerializeField] [Range(0, 10)] private float _walkAcceleration;
    [SerializeField] [Range(0, 10)] private float _runningAcceleration;
    [SerializeField] [Range(0, 10)] private float _sneakAcceleration;
    [SerializeField] [Range(0, 10)] private float _jumpAcceleration;
    
    [SerializeField] [Range(0, 20)] private float _maxWalkSpeed;
    [SerializeField] [Range(0, 20)] private float _maxRunningSpeed;
    [SerializeField] [Range(0, 20)] private float _maxSneakingSpeed;
    [SerializeField] [Range(0, 20)] private float _maxJumpHeight;

    private float _shootTimer;
    private bool _isShooting;
    private CharacterStateMachine _stateMachine;
    private CharacterMovements _characterMovements;
    private PlayerInputs _playerInputs;
    private Rigidbody _rbody;
    private MeshRenderer _renderer;
    private float[] movementValues;

    void Awake()
    {
        movementValues = new float[8]
        {
            _walkAcceleration, _maxWalkSpeed, _runningAcceleration, _maxRunningSpeed, _sneakAcceleration,
            _maxSneakingSpeed, _jumpAcceleration, _maxJumpHeight
        };

        _shootTimer = 0f;
        _rbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _stateMachine = new CharacterStateMachine(this);
        _playerInputs = new PlayerInputs();
        _characterMovements = new CharacterMovements(_rbody, movementValues);
    }

    private void Update()
    {
        _stateMachine.CurrentInput = _playerInputs.GetKeys(_stateMachine.currentInput);
        _isShooting = _playerInputs.GetClick();
        _shootTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        switch (currentPlayerState)
        {
            case State.Idle: _renderer.material.SetColor("_Color", Color.white);
                break;
            case State.Crouching: _renderer.material.SetColor("_Color", Color.yellow);
                break;
            case State.Walking: _characterMovements.Walk();
                _renderer.material.SetColor("_Color", Color.cyan);
                break;
            case State.Running: _characterMovements.Run();
                _renderer.material.SetColor("_Color", Color.magenta);
                break;
            case State.Sneaking: _characterMovements.Sneak();
                _renderer.material.SetColor("_Color", Color.blue);
                break;
        }

        if (_isShooting && _shootTimer > 0.3f)
        {
            GameObject go = BulletPool.instance.ObjectPoolSpawn();
            go.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            _shootTimer = 0f;
        }
    }
}
