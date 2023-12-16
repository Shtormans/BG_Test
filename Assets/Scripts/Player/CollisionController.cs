using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DeadZone deadZone))
        {
            ReactToDeadZone();
        }
        else if (other.TryGetComponent(out FinishZone finishZone))
        {
            ReactToFinishZone();
        }
    }

    public void ReactToDeadZone()
    {
        _playerController.Lose();
    }

    public void ReactToFinishZone()
    {
        _playerController.Win();
    }
}
