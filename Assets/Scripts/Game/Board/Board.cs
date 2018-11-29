using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public int rows;
    public int cols;
    private Tile[,] tiles;

    public GameObject emptyTile;
    public float tileWidth;
    public float tileHeight;

    public static void MoveUnit(Unit unit, Tile targetTile) {
        Tile start = unit.tile;
        if(start.unit == unit){
            start.RemoveUnit(unit);
        }
        targetTile.PlaceUnit(unit);
    }

    public static void MoveUnit(SimulatedDisplacement sim, Board board) {
        MoveUnit(sim.GetUnit(), board.GetTile(sim.GetCurrentVector()));
    }

    public void InitializeBoard()
    {
        tiles = new Tile[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                GameObject tileObj = Instantiate(emptyTile, gameObject.transform);
                Tile tile = tileObj.GetComponent<Tile>();
                PlaceTile(tile, i, j);
            }
        }
    }

    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3((y-x)*tileWidth, (x+y)*-tileHeight, 0);
    }

	public Vector3 CoordToPosition(Vector2 coord)
	{
		return new Vector3((coord.y - coord.x) * tileWidth, (coord.x + coord.y) * -tileHeight, 0);
	}

	public Tile GetTile(int x, int y) {
        return tiles[x, y];
    }

    public Tile GetTile(Vector2 coord) {
        return tiles[(int)coord.x, (int)coord.y];
    }

    public bool CheckCoord(int x, int y) {
        return (x >= 0 && x < rows &&
            y >= 0 && y < cols);
    }

    public bool CheckCoord(Vector2 coord) {
        int x = (int)coord.x;
        int y = (int)coord.y;

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

    public bool CheckPlacePiece(Piece piece, Vector2 origin) {
        int x = (int)origin.x;
        int y = (int)origin.y;

        for(int i = 0; i < piece.row; i++) {
            for(int j = 0; j < piece.cols; j++) {
                if(piece.tiles[i,j] != null && !CheckPlaceTile(i + x, j + y)) {
                    return false;
                }
            }
        }

        return true;
    }

    public void PlacePiece(Piece piece, Vector2 origin) {
        if(CheckPlacePiece(piece, origin)) {
            int x = (int)origin.x;
            int y = (int)origin.y;

            for(int i = 0; i < piece.row; i++) {
                for(int j = 0; j < piece.cols; j++) {
                    PlaceTile(piece.tiles[i, j], i + x, j + y);
                }
            }
        }
    }

    public void PlacePieceAsPossible(Piece piece, Vector2 origin) {
        int x = (int)origin.x;
        int y = (int)origin.y;

        for(int i = 0; i < piece.row; i++) {
            for(int j = 0; j < piece.cols; j++) {
                if(piece.tiles[i,j] != null && CheckPlaceTile(i + x, j + y)) {
                    PlaceTile(piece.tiles[i, j], i + x, j + y);
                }
            }
        }
    }

    public bool CheckPlaceTile(int x, int y)
    {
        return CheckCoord(x, y) && this.tiles[x, y].tileType == emptyTile.GetComponent<Tile>().tileType;
    }

    public void PlaceTile(Tile tile, int x, int y)
    {
        tile.coordinate = new Vector2(x, y);
        tile.transform.position = CoordToPosition(x, y);
        tile.transform.parent = this.transform;
        if (this.tiles[x,y] != null)
        {
            Destroy(this.tiles[x, y].gameObject);
        }
        this.tiles[x,y] = tile;
    }
}
