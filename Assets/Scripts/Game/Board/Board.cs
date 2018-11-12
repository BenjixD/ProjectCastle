using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int rows;
    public int cols;
    private Tile[,] tiles;

    public GameObject baseTile;         // Base tile for testing
    public float tileWidth;
    public float tileHeight;

    public void InitializeBoard()
    {
        tiles = new Tile[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tileObj = Instantiate(baseTile, CoordToPosition(i, j), Quaternion.identity, gameObject.transform);
                Tile tile = tileObj.GetComponent<Tile>();
                tile.coordinate = new Vector2(i, j); 
                tiles[i, j] = tile;
            }
        }
    }

    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3((y-x)*tileWidth, (x+y)*-tileHeight, 0);
    }

    public Tile GetTile(int x, int y) {
        return tiles[x, y];
    }

    public bool CheckCoord(int x, int y) {
        return (x >= 0 && x < rows &&
            y >= 0 && y < cols);
    }

    public Dictionary<Direction, Tile> GetNeighbours(Tile tile) {
        Dictionary<Direction, Tile> neighbours = new Dictionary<Direction, Tile>();

        //Left
        if(CheckCoord((int)tile.coordinate.x, (int)tile.coordinate.y - 1)) {
            neighbours.Add(Direction.LEFT, tiles[(int)tile.coordinate.x, (int)tile.coordinate.y  - 1]);
        }
        //right
        if(CheckCoord((int)tile.coordinate.x, (int)tile.coordinate.y + 1)) {
            neighbours.Add(Direction.RIGHT, tiles[(int)tile.coordinate.x, (int)tile.coordinate.y + 1]);
        }
        //down
        if(CheckCoord((int)tile.coordinate.x - 1, (int)tile.coordinate.y)) {
            neighbours.Add(Direction.DOWN, tiles[(int)tile.coordinate.x - 1, (int)tile.coordinate.y]);
        }
        //up
        if(CheckCoord((int)tile.coordinate.x + 1, (int)tile.coordinate.y)) {
            neighbours.Add(Direction.UP, tiles[(int)tile.coordinate.x + 1, (int)tile.coordinate.y]);
        }

        return neighbours;
    } 
}
