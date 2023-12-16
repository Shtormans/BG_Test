using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _secondsToStartMoving = 2f;
    [SerializeField] private Vector3 _destination;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartMovingInSecondsCoroutine());
    }

    public void SetDestination(Vector3 destination)
    {
        _destination = destination;
    }

    public void StartMoving()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_destination);
    }

    private IEnumerator StartMovingInSecondsCoroutine()
    {
        yield return new WaitForSeconds(_secondsToStartMoving);

        StartMoving();
    }
}
