using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject tile;         // Base tile for testing
    public float tileWidth;
    public float tileHeight;

    void Start()
    {
        DrawBoard();
    }

    // Test drawing the board
    public static void DrawBoard(int rows, int cols)
    {
        foreach (int i in rows)
        {
            foreach (int j in cols)
            {
                Instantiate(tile, CoordsToPosition(i, j));
            }
        }
    }

    public static Vector2 CoordsToPosition(int x, int y)
    {
        return new Vector2((y-x)*tileWidth, (x+y)*-tileHeight);
    }
}
