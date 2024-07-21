using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;

    private Rigidbody _rigidBody;
    private NewInput _input;
    private Animator _animator;
    private Vector2 _animationSmooth;
    private float _currentMaxSpeed;
    private HasteBuffComponent _currentBuff;

    private float CurrentSpeed
    {
        get => _currentMaxSpeed;
        set
        {
            _currentMaxSpeed = value;
            StartCoroutine(BuffTask());
        }
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _input = new NewInput();
        _currentMaxSpeed = _maxSpeed;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        var moveValue = _input.Player.Movement.ReadValue<Vector2>();

        if(moveValue.sqrMagnitude > 0)
        {
            var force = new Vector3(moveValue.x, 0, moveValue.y) * _acceleration * Time.fixedDeltaTime;

            _animationSmooth = Vector2.MoveTowards(_animationSmooth, moveValue, _acceleration * Time.fixedDeltaTime);
            _rigidBody.AddForce(transform.rotation * force);


            if(_rigidBody.velocity.magnitude > CurrentSpeed)
            {
                _rigidBody.velocity = transform.rotation * new Vector3(moveValue.x, 0, moveValue.y) * CurrentSpeed;
            }
        }
        else
        {
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, Vector3.zero, _deceleration * Time.fixedDeltaTime);
            _animationSmooth = Vector2.MoveTowards(_animationSmooth, Vector2.zero, _deceleration * Time.fixedDeltaTime);
        }

        _animator?.SetFloat(Constants.Animation.SpeedX, _animationSmooth.x);
        _animator?.SetFloat(Constants.Animation.SpeedY, _animationSmooth.y);
    }

    public void TakeBuff(HasteBuffComponent hasteBuffComponent)
    {
        _currentBuff = hasteBuffComponent;
        CurrentSpeed = _maxSpeed * hasteBuffComponent.HasteBonus;
    }

    private IEnumerator BuffTask()
    {
        var elapsedTime = 0f;

        while(elapsedTime < _currentBuff.Duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentMaxSpeed = _maxSpeed;
    }
}
