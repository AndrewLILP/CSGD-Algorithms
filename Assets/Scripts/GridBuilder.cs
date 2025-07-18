using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab; // Prefab for the tile

    [SerializeField] private float distanceBetweenTiles = 4f; // Distance between tiles
    public int rowSize = 5;
    public int columnSize = 5;

    private Dictionary<Vector2, TileController> gridTiles = new Dictionary<Vector2, TileController>();

    private void Awake()
    {
        BuildGrid();
    }

    /// <summary>
    /// technically an algorithm to build a grid of tiles
    /// Big O (linear) Notation: O(n*m) where n is the number of rows and m is the number of columns
    /// 
    private void Buildrow(int rowIndex, int length)
    {
        for (int x = 0; x < length; x++)
        {
            Vector3 position = new Vector3(x * distanceBetweenTiles, 0, rowIndex * distanceBetweenTiles);
            TileController tileClone = Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<TileController>();


            // Store in dictionary and set tile index
            Vector2 gridPosition = new Vector2(x, rowIndex);
            gridTiles[gridPosition] = tileClone;
            tileClone.tileIndex = rowIndex * rowSize + x; // Optional: set a linear index

        }
    }
    // Big O (linear) Notation: O(n) where n is the number of rows - but with a algorithm inside
    // Big 0 (n^2) Notation: O(n*m) where n is the number of rows and m is the number of columns

    private void BuildGrid()
    {
        for (int z = 0; z < columnSize; z++)
        {
            Buildrow(z, rowSize);
        }
    }
}
