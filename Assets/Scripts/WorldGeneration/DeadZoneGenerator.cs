using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneGenerator : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] private float _secondsToGenerate = 2f;
    [SerializeField] private MazeGenerator _mazeGenerator;
    [SerializeField] private GameObject _deadZone;

    private Queue<GameObject> _deadZones;
    private Coroutine _generatingCoroutine;

    private void Awake()
    {
        _deadZones = new Queue<GameObject>();
    }

    private void OnDisable()
    {
        StopCoroutine(GenerateZoneCoroutine());
    }

    public void StartGenerating()
    {
        _deadZones.Clear();

        _generatingCoroutine = StartCoroutine(GenerateZoneCoroutine());
    }

    public void StopGenerating()
    {
        StopCoroutine(_generatingCoroutine);
    }

    public void DeleteAllDeadZones()
    {
        while (_deadZones.Count != 0)
        {
            var deadZone = _deadZones.Dequeue();
            Destroy(deadZone);
        }
    }

    private IEnumerator GenerateZoneCoroutine()
    {
        bool[,] alreadyCreated = new bool[_mazeGenerator.MazeWidth, _mazeGenerator.MazeDepth];
        alreadyCreated[0, 0] = true;
        alreadyCreated[_mazeGenerator.MazeWidth - 1, _mazeGenerator.MazeDepth - 1] = true;

        int attempts = 0;

        yield return new WaitForSeconds(_secondsToGenerate);

        while (true)
        {
            int x = Random.Range(0, _mazeGenerator.MazeWidth);
            int y = Random.Range(0, _mazeGenerator.MazeDepth);

            if (alreadyCreated[x, y])
            {
                attempts++;
                if (attempts > _mazeGenerator.MazeWidth * _mazeGenerator.MazeDepth)
                {
                    yield break;
                }

                continue;
            }

            attempts = 0;
            alreadyCreated[x, y] = true;
            var deadZone = Instantiate(_deadZone, _mazeGenerator.GetCellPosition(new Vector2(x, y)), Quaternion.identity);
            _deadZones.Enqueue(deadZone);

            yield return new WaitForSeconds(_secondsToGenerate);
        }
    }
}
