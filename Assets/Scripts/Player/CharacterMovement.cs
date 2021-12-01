using UnityEngine;

public struct CharacterMovement
{
    private float _walkAcceleration;
    private float _runningAcceleration;
    private float _sneakAcceleration;
    private float _jumpAcceleration;
    private float _maxWalkSpeed;
    private float _maxRunningSpeed;
    private float _maxSneakingSpeed;
    private float _maxJumpHeight;
    private Vector3 _currentDirection;
    private Vector3 _currentVelocity;
    private Rigidbody _rb;

    public CharacterMovement(Rigidbody rigidbody, float[] movementValues)
    {
        _currentDirection = new Vector3(0, 0, 0);
        _currentVelocity = new Vector3(0, 0, 0);
        
        _walkAcceleration = movementValues[0];
        _maxWalkSpeed = movementValues[1];
        _runningAcceleration = movementValues[2];
        _maxRunningSpeed = movementValues[3];
        _sneakAcceleration = movementValues[4];
        _maxSneakingSpeed = movementValues[5];
        _jumpAcceleration = movementValues[6];
        _maxJumpHeight = movementValues[7];

        _rb = rigidbody;
    }

    public void Walk()
    {
        _currentDirection.x = Input.GetAxisRaw("Horizontal");
        _currentDirection.z = Input.GetAxisRaw("Vertical");
        _currentVelocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0, _maxWalkSpeed), 0,
            Mathf.Clamp(_rb.velocity.z, 0, _maxWalkSpeed));

        if (_currentVelocity.x < _maxWalkSpeed || _currentVelocity.z < _maxWalkSpeed)
        {
            _currentVelocity.x += _walkAcceleration * _currentDirection.x;
            _currentVelocity.z += _walkAcceleration * _currentDirection.z;
        }

        _rb.velocity = _currentVelocity;
    }

    public void Run()
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

    public void Sneak()
    {
        _currentDirection.x = Input.GetAxisRaw("Horizontal");
        _currentDirection.z = Input.GetAxisRaw("Vertical");
        _currentVelocity = new Vector3(Mathf.Clamp(_rb.velocity.x, 0, _maxSneakingSpeed), 0,
            Mathf.Clamp(_rb.velocity.z, 0, _maxSneakingSpeed));

        if (_currentVelocity.x < _maxSneakingSpeed || _currentVelocity.z < _maxRunningSpeed)
        {
            _currentVelocity.x += _sneakAcceleration * _currentDirection.x;
            _currentVelocity.z += _sneakAcceleration * _currentDirection.z;
        }

        _rb.velocity = _currentVelocity;
    }
}