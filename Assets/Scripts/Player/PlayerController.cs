using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnLost;
    public event Action OnWon;

    private PlayerMover _playerMover;
    private ShieldController _shieldController;
    private bool _isImmortal;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _shieldController = GetComponent<ShieldController>();

        _isImmortal = false;
    }

    private void OnEnable()
    {
        _shieldController.ShieldActivated += MakeImmortal;
        _shieldController.ShieldDeactivated += MakeMortal;
    }

    private void OnDisable()
    {
        _shieldController.ShieldActivated -= MakeImmortal;
        _shieldController.ShieldDeactivated -= MakeMortal;
    }

    public void SetDestination(Vector3 destination)
    {
        _playerMover.SetDestination(destination);
    }

    public void Lose()
    {
        if (_isImmortal)
        {
            return;
        }
        
        OnLost?.Invoke();
        gameObject.SetActive(false);
    }

    public void Win()
    {
        OnWon?.Invoke();
    }

    private void MakeImmortal()
    {
        _isImmortal = true;
    }

    private void MakeMortal()
    {
        _isImmortal = false;
    }
}
