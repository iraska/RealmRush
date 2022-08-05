using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // If you only working a square grids, use integer
    [SerializeField] Vector2Int gridSize;

    // Create a dictionay; our key is coordinates so it's Vector2Int, in case value is node and dicionary name is grid
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    void Awake() 
    {
        CreateGrid();
    }

// We can access elements within our grid, we can use our GetNode method
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true)); // true: always walkable
            }
        }
    }
}
