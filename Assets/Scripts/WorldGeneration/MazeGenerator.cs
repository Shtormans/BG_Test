using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _mazeCellPrefab;
    [SerializeField] private Floor _floor;

    private int _mazeWidth;
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;
    private const float _accuracy = 1;
    private const float _cellSize = 2;

    public Vector3 UpperLeftCell => _mazeGrid[_mazeWidth - 1, 0].Position;
    public Vector3 UpperRightCell => _mazeGrid[_mazeWidth - 1, _mazeDepth - 1].Position;
    public Vector3 BottomLeftCell => _mazeGrid[0, 0].Position;
    public Vector3 BottomRightCell => _mazeGrid[0, _mazeDepth - 1].Position;

    public int MazeWidth => _mazeWidth;
    public int MazeDepth => _mazeDepth;

    public void DestroyMaze()
    {
        if (_mazeGrid == null)
        {
            return;
        }

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                Destroy(_mazeGrid[x, z].gameObject);
            }
        }
    }

    public void StartGenerating()
    {
        _mazeWidth = (int)(_floor.Size.x / _cellSize);
        _mazeDepth = (int)(_floor.Size.z / _cellSize);

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(_floor.BottomLeftCorner.x + x * _cellSize + _accuracy, _floor.BottomLeftCorner.y, _floor.BottomLeftCorner.z + z * _cellSize + _accuracy), Quaternion.identity);
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);
    }

    public Vector3 GetCellPosition(Vector2 coords)
    {
        return _mazeGrid[(int)coords.x, (int)coords.y].transform.position;
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(cell => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)(currentCell.transform.position.x - _floor.BottomLeftCorner.x - _accuracy) / (int)_cellSize;
        int z = (int)(currentCell.transform.position.z - _floor.BottomLeftCorner.z - _accuracy) / (int)_cellSize;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (!cellToRight.IsVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (!cellToLeft.IsVisited)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (!cellToFront.IsVisited)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (!cellToBack.IsVisited)
            {
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
