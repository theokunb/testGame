using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private NewInput _input;
    private Camera _camera;
    private Mouse _mouse;

    private void Awake()
    {
        _input = new NewInput();
        _camera = Camera.main;
        _mouse = Mouse.current;
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
        var mouseDown = _input.Player.LeftMouse.ReadValue<float>();

        if (mouseDown == 0)
        {
            return;
        }

        var clickPoint = _camera.ScreenToWorldPoint(_mouse.position.value);

        var direction = new Vector3(clickPoint.x, transform.position.y, clickPoint.z) - transform.position;
        var rotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
    }
}
