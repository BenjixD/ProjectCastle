using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject tile;         // Base tile for testing
    public float tileWidth;
    public float tileHeight;

    void Start()
    {
        DrawBoard(20, 20);
    }

    // Test drawing the board
    public void DrawBoard(int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Instantiate(tile, CoordsToPosition(i, j), Quaternion.identity);
            }
        }
    }

    public Vector3 CoordsToPosition(int x, int y)
    {
        return new Vector3((y-x)*tileWidth, (x+y)*-tileHeight, 0);
    }
}
