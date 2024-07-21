using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponHold : MonoBehaviour, IWeaponVisitor
{
    private Animator _animator;

    private Animator _currentAnimator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }

            return _animator;
        }
    }

    public void Visit(Pistol bulletGun)
    {
        _currentAnimator?.SetLayerWeight(Constants.Animation.PistolLayer, 1);
        _currentAnimator?.SetLayerWeight(Constants.Animation.GranadeLauncherLayer, 0);
    }

    public void Visit(Shotgun shotgun)
    {
        _currentAnimator?.SetLayerWeight(Constants.Animation.PistolLayer, 0);
        _currentAnimator?.SetLayerWeight(Constants.Animation.GranadeLauncherLayer, 0);
    }

    public void Visit(AutoGun autoGun)
    {
        _currentAnimator?.SetLayerWeight(Constants.Animation.PistolLayer, 0);
        _currentAnimator?.SetLayerWeight(Constants.Animation.GranadeLauncherLayer, 0);
    }

    public void Visit(GranadeLauncher granadeLauncher)
    {
        _currentAnimator?.SetLayerWeight(Constants.Animation.GranadeLauncherLayer, 1);
        _currentAnimator?.SetLayerWeight(Constants.Animation.PistolLayer, 0);
    }
}

public interface IWeaponVisitor
{
    void Visit(Pistol pistol);
    void Visit(AutoGun autoGun);
    void Visit(Shotgun shotgun);
    void Visit(GranadeLauncher granadeLauncher);
}