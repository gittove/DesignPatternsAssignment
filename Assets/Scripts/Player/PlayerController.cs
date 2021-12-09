using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(0, 10)] private float _walkAcceleration;
    [SerializeField] [Range(0, 10)] private float _runningAcceleration;
    [SerializeField] [Range(0, 10)] private float _sneakAcceleration;
    [SerializeField] [Range(0, 10)] private float _jumpAcceleration;
    
    [SerializeField] [Range(0, 20)] private float _maxWalkSpeed;
    [SerializeField] [Range(0, 20)] private float _maxRunningSpeed;
    [SerializeField] [Range(0, 20)] private float _maxSneakingSpeed;

    private float _shootTimer;
    private float _groundCheckTimer;
    private float _groundCheckDelay;
    private bool _isShooting;
    private bool _justJumped;
    private bool _isGrounded;
    private State _currentPlayerState;
    private CharacterStateMachine _stateMachine;
    private CharacterMovements _characterMovements;
    private PlayerInputHandler _playerInputHandler;
    private Rigidbody _rbody;
    private MeshRenderer _renderer;
    private CapsuleCollider _collider;
    private float[] movementValues;

    [SerializeField] LayerMask _groundLayers;

    public State CurrentPlayerState
    {
        set
        {
            if (value != _currentPlayerState)
            {
                _currentPlayerState = value;
                CheckForJumpingState();
            }
        }
    }

    void Awake()
    {
        movementValues = new float[7]
        {
            _walkAcceleration, _maxWalkSpeed, _runningAcceleration, _maxRunningSpeed, _sneakAcceleration,
            _maxSneakingSpeed, _jumpAcceleration
        };

        _shootTimer = 0f;
        _groundCheckTimer = 0f;
        _groundCheckDelay = 0.1f;
        _justJumped = false;
        _rbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<CapsuleCollider>();
        _stateMachine = new CharacterStateMachine(this);
        _playerInputHandler = new PlayerInputHandler();
        _characterMovements = new CharacterMovements(_rbody, movementValues);
    }

    private void Update()
    {
        _stateMachine.CurrentInput = _playerInputHandler.GetKeys(_stateMachine.currentInput, _currentPlayerState);
        _isShooting = _playerInputHandler.GetClick();
        _shootTimer += Time.deltaTime;

        if (_justJumped)
        {
            _groundCheckTimer += Time.deltaTime;
            if (_groundCheckTimer > _groundCheckDelay)
            {
                _justJumped = false;
                _groundCheckTimer = 0.0f;
            }
        }

    }

    private void FixedUpdate()
    {
        switch (_currentPlayerState)
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
            GameObject go = ObjectPool.instance.ObjectPoolSpawn();
            go.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1);
            _shootTimer = 0f;
        }
        if(!_justJumped)
            GroundCheck();
    }

    void CheckForJumpingState()
    {
        if (_currentPlayerState == State.Jumping)
        {
            _renderer.material.SetColor("_Color",Color.red);
            _characterMovements.Jump();
            _justJumped = true;
        }
    }
    
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _collider.height * 0.5f, _groundLayers))
        {
            if (_currentPlayerState == State.Jumping)
            {
                _stateMachine.ReturnToPreviousState();
            }
        }
    }
}
