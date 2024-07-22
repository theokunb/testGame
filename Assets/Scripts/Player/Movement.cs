using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private AudioClip[] _steps;

    [Inject] private SoundContainer _container;

    private Rigidbody _rigidBody;
    private NewInput _input;
    private Animator _animator;
    private Vector2 _animationSmooth;
    private float _currentMaxSpeed;
    private float _speedModificator = 1;

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


            if(_rigidBody.velocity.magnitude > _currentMaxSpeed)
            {
                _rigidBody.velocity = transform.rotation * new Vector3(moveValue.x, 0, moveValue.y) * _currentMaxSpeed * _speedModificator;
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
        StartCoroutine(BuffTask(hasteBuffComponent.HasteBonus, hasteBuffComponent.Duration));
    }


    private bool isEternalSlow;
    public void TakeSlow(SlowZone slowZone)
    {
        isEternalSlow = true;
        StartCoroutine(BuffTask(slowZone.SlowFactor));
    }

    public void RemoveSlow(SlowZone slowZone)
    {
        isEternalSlow = false;
    }

    public void OnStepSound()
    {
        int rand = UnityEngine.Random.Range(0, _steps.Count());

        _container.ConfigureFreeAudioSource(source =>
        {
            source.clip = _steps[rand];
            source.volume = 0.2f;
            source.loop = false;
        });
    }

    private IEnumerator BuffTask(float speedModificator, float duration = 0f)
    {
        var elapsedTime = 0f;
        _speedModificator *= speedModificator;

        while (elapsedTime < duration || isEternalSlow)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _speedModificator /= speedModificator;
    }
}
