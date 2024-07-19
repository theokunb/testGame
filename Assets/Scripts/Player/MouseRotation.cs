using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private NewInput _input;
    private Coroutine _rotationTask;

    private void Awake()
    {
        _input = new NewInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.LeftMouse.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.LeftMouse.performed -= OnMouseClick;
    }


    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        var mouse = Mouse.current;
        var camera = Camera.main;
        var clickPoint = camera.ScreenToWorldPoint(mouse.position.value);

        var direction = new Vector3(clickPoint.x, transform.position.y, clickPoint.z) - transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if(_rotationTask != null)
        {
            StopCoroutine(_rotationTask);
        }

        _rotationTask = StartCoroutine(RotationTask(rotation));
    }

    private IEnumerator RotationTask(Quaternion rotation)
    {
        while(transform.rotation != rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
