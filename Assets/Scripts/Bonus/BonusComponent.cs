using UnityEngine;

public abstract class BonusComponent : MonoBehaviour
{
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
        TakeAction(other);
    }

    protected abstract void TakeAction(Collider other);
}
