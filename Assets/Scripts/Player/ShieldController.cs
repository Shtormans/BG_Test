using System;
using System.Collections;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float _shieldDuration = 2f;

    public event Action ShieldActivated;
    public event Action ShieldDeactivated;

    public void ActivateShield()
    {
        StartCoroutine(DeactivateShieldInSeconds());

        ShieldActivated?.Invoke();
    }

    public void DeactivateShield()
    {
        ShieldDeactivated?.Invoke();
    }

    private IEnumerator DeactivateShieldInSeconds()
    {
        yield return new WaitForSeconds(_shieldDuration);

        DeactivateShield();
    }
}
