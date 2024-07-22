using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float _viewingAngle;
    [SerializeField] private Weapon _defaultWeapon;

    [Inject] WeaponService _weaponService;

    private NewInput _input;
    private Vector3 _shootDirection;
    private bool _canShoot;
    private Mouse _mouse;
    private Camera _camera;


    private void Awake()
    {
        _camera = Camera.main;
        _mouse = Mouse.current;
        _input = new NewInput();
        _shootDirection = transform.forward;
        _canShoot = false;

        _weaponService.Equip(_defaultWeapon);
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
        CanShoot();

        var mouseVal = _input.Player.LeftMouse.ReadValue<float>();

        if (mouseVal == 1)
        {
            OnLeftMouse();
        }
    }

    private void CanShoot()
    {
        var rightRotation = Quaternion.AngleAxis(_viewingAngle, transform.up);
        var leftRotation = Quaternion.AngleAxis(-_viewingAngle, transform.up);

        var right = rightRotation * transform.forward;
        var left = leftRotation * transform.forward;

        Debug.DrawRay(transform.position, right, Color.red);
        Debug.DrawRay(transform.position, left, Color.red);
        Debug.DrawRay(transform.position, _shootDirection, Color.blue);

        var targetRotation = Quaternion.LookRotation(_shootDirection);
        rightRotation = Quaternion.LookRotation(right);
        leftRotation = Quaternion.LookRotation(left);

        _canShoot = CompareAngles(targetRotation.eulerAngles.y, leftRotation.eulerAngles.y) &&
            CompareAngles(rightRotation.eulerAngles.y, targetRotation.eulerAngles.y);
    }

    /// <summary>
    /// return eulerAngle1 greater than eulerAngle2
    /// </summary>
    /// <param name="eulerAngle1"></param>
    /// <param name="eulerAngle2"></param>
    /// <returns></returns>
    private bool CompareAngles(float eulerAngle1, float eulerAngle2)
    {
        var diff = Mathf.Abs(eulerAngle1 - eulerAngle2);

        if(diff < 180)
        {
            return eulerAngle1 > eulerAngle2;
        }

        eulerAngle1 = eulerAngle1 % 180;
        eulerAngle2 = eulerAngle2 % 180;

        return eulerAngle2 > eulerAngle1;
    }

    private void OnLeftMouse()
    {
        var clickPoint = _camera.ScreenToWorldPoint(_mouse.position.value);
        _shootDirection = (new Vector3(clickPoint.x, transform.position.y, clickPoint.z) - transform.position);

        if (_canShoot)
        {
            _weaponService.Shoot(_shootDirection);
        }
    }

    public void Equip(Weapon weapon)
    {
        _weaponService.Equip(weapon);
    }
}
