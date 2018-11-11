using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int rows;
    public int cols;
    private GameObject[,] tiles;

    public GameObject baseTile;         // Base tile for testing
    public float tileWidth;
    public float tileHeight;

    void Start()
    {
        InitializeBoard();
    }

    public void InitializeBoard()
    {
        tiles = new GameObject[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tile = Instantiate(baseTile, CoordsToPosition(i, j), Quaternion.identity, gameObject.transform);
                tiles[i, j] = tile;
            }
        }
    }

    public Vector3 CoordsToPosition(int x, int y)
    {
        return new Vector3((y-x)*tileWidth, (x+y)*-tileHeight, 0);
    }
}
