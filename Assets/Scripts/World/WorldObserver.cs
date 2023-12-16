using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class WorldObserver : MonoBehaviour
{
    [SerializeField] private float _lostAnimationInSeconds = 5f;

    [SerializeField] private MazeGenerator _mazeGenerator;
    [SerializeField] private DeadZoneGenerator _deadZoneGenerator;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private Floor _floor;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _finishZonePrefab;
    [SerializeField] private FadingAnimation _fadeAnimation;

    private void OnEnable()
    {
        _player.OnLost += OnPlayerDied;
        _player.OnWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        _player.OnLost -= OnPlayerDied;
        _player.OnWon -= OnPlayerWon;
    }

    private void Start()
    {
        StartGameWithNewMaze();
    }

    private void StartGameWithNewMaze()
    {
        _mazeGenerator.DestroyMaze();
        _mazeGenerator.StartGenerating();

        StartWithAlreadyCreatedMaze();
    }

    private void StartWithAlreadyCreatedMaze()
    {
        _navMeshSurface.BuildNavMesh();

        var finishZoneGameObject = Instantiate(_finishZonePrefab, _mazeGenerator.UpperRightCell, Quaternion.identity);

        _player.transform.position = _mazeGenerator.BottomLeftCell;
        _player.gameObject.SetActive(true);
        _player.SetDestination(finishZoneGameObject.transform.position);

        _deadZoneGenerator.DeleteAllDeadZones();
        _deadZoneGenerator.StartGenerating();
    }

    private void OnPlayerDied()
    {
        _deadZoneGenerator.StopGenerating();

        StartCoroutine(PlayLostAnimation());
    }

    private void OnPlayerWon()
    {
        _deadZoneGenerator.StopGenerating();

        StartCoroutine(PlayWonAnimation());
    }

    private IEnumerator PlayLostAnimation()
    {
        yield return new WaitForSeconds(_lostAnimationInSeconds);

        StartWithAlreadyCreatedMaze();
    }

    private IEnumerator PlayWonAnimation()
    {
        _fadeAnimation.FadeIn();

        yield return null;

        yield return new WaitForSeconds(_fadeAnimation.DurationInSeconds);

        _player.gameObject.SetActive(false);

        yield return null;

        StartGameWithNewMaze();

        yield return null;

        _navMeshSurface.BuildNavMesh();

        _fadeAnimation.FadeOut();
    }
}
