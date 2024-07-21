using UnityEngine;
using Zenject;

public class WeaponSound : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private AudioClip _pistol;
    [SerializeField] private AudioClip _autogun;
    [SerializeField] private AudioClip _shotgun;

    [Inject] private SoundContainer _soundContainer;

    public void Visit(Pistol pistol)
    {
        _soundContainer.ConfigureFreeAudioSource(source =>
        {
            source.clip = _pistol;
            source.volume = 0.5f;
            source.loop = false;
        });
    }

    public void Visit(AutoGun autoGun)
    {
        _soundContainer.ConfigureFreeAudioSource(source =>
        {
            source.clip = _autogun;
            source.volume = 0.5f;
            source.loop = false;
        });
    }

    public void Visit(Shotgun shotgun)
    {
        _soundContainer.ConfigureFreeAudioSource(source =>
        {
            source.clip = _shotgun;
            source.volume = 0.5f;
            source.loop = false;
        });
    }
}
