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

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _input = new NewInput();
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
            _rigidBody.AddForce(force);


            if(_rigidBody.velocity.magnitude > _maxSpeed)
            {
                _rigidBody.velocity = new Vector3(moveValue.x, 0, moveValue.y) * _maxSpeed;
            }
        }
        else
        {
            _rigidBody.velocity = Vector3.MoveTowards(_rigidBody.velocity, Vector3.zero, _deceleration * Time.fixedDeltaTime);
        }

        Debug.Log(_rigidBody.velocity.x / _maxSpeed);
        Debug.Log(_rigidBody.velocity.y / _maxSpeed);

        _animator?.SetFloat(Constants.Animation.SpeedX, _rigidBody.velocity.x / _maxSpeed);
        _animator?.SetFloat(Constants.Animation.SpeedY, _rigidBody.velocity.z / _maxSpeed);
    }
}
