using UnityEngine;

public class BonusComponent : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    
    private float _elapsedTime;

    public float LifeTime { get; set; }

    private void Start()
    {
        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;

        if(_elapsedTime >= LifeTime)
        {
            _elapsedTime = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Shoot shoot))
        {
            shoot.Equip(_weapon);
            Destroy(gameObject);
        }
    }
}
