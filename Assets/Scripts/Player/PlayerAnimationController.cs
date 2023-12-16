using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _shieldMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private GameObject _destroyedPlayer;

    [SerializeField, Range(0f, 5f)] private float _winningAnimationSeconds = 2f;
    [SerializeField, Range(0, 10)] private float _winningAnimationShots = 3f;

    private ShieldController _shieldController;
    private PlayerController _playerController;

    private void Awake()
    {
        _shieldController = GetComponent<ShieldController>();
        _playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _shieldController.ShieldActivated += PlayShieldOnAnimation;
        _shieldController.ShieldDeactivated += PlayShieldOffAnimation;

        _playerController.OnLost += PlayDyingAnimation;
        _playerController.OnWon += PlayWinningAnimation;
    }

    private void OnDisable()
    {
        _shieldController.ShieldActivated -= PlayShieldOnAnimation;
        _shieldController.ShieldDeactivated -= PlayShieldOffAnimation;
    
        _playerController.OnLost -= PlayDyingAnimation;
        _playerController.OnWon -= PlayWinningAnimation;
    }

    private void PlayShieldOnAnimation()
    {
        _meshRenderer.material = _shieldMaterial;
    }

    private void PlayShieldOffAnimation()
    {
        _meshRenderer.material = _defaultMaterial;
    }

    private void PlayDyingAnimation()
    {
        Instantiate(_destroyedPlayer, transform.position, Quaternion.identity);
    }

    private void PlayWinningAnimation()
    {
        StartCoroutine(PlayWinningCoroutine());
    }

    private IEnumerator PlayWinningCoroutine()
    {
        for (int i = 0; i < _winningAnimationShots; i++)
        {
            var position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            Instantiate(_destroyedPlayer, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_winningAnimationSeconds);
        }
    }
}
