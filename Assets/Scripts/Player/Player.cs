using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isInvincible = false;
    private InvincibileBuffComponent _currentBuff;

    private bool IsInvincible
    {
        get => _isInvincible;
        set
        {
            _isInvincible = value;
            StartCoroutine(InvincibleTask());
        }
    }

    public void Die()
    {
        if(IsInvincible == true)
        {
            return;
        }

        Debug.Log("died");
    }

    private IEnumerator InvincibleTask()
    {
        var elapsedTime = 0f;

        while(elapsedTime < _currentBuff.Duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsInvincible = false;
    }

    public void TakeBuff(InvincibileBuffComponent buff)
    {
        _currentBuff = buff;
        IsInvincible = true;
    }
}
