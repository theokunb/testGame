using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private AudioClip _clip;

    private float _elapsedTime;
    private SoundContainer _container;

    private void Awake()
    {
        _elapsedTime = 0;

        _container = FindAnyObjectByType<SoundContainer>();
    }

    private void Start()
    {
        _container?.ConfigureFreeAudioSource(source =>
        {
            source.clip = _clip;
            source.volume = 0.3f;
            source.loop = false;
        });
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.fixedDeltaTime;
        
        if (_elapsedTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
