using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public string pieceName;

    public int row;
    public int cols;
    public Board board;
	public Tile[,] tiles;

	void Start() {
    	//Set tiles appropriately
    	tiles = new Tile[row, cols];
    	foreach(Tile tile in GetComponentsInChildren<Tile>())
		{
			Debug.Log(tile.coordinate.x + ", " + tile.coordinate.y);
		    tiles[(int)tile.coordinate.x, (int)tile.coordinate.y] = tile;
		}
		UpdatePosition();
    }

    void UpdatePosition() {
    	//Move tiles to their corresponding locations
		for(int i = 0; i < tiles.GetLength(0); i++) {
			for(int j = 0; j < tiles.GetLength(1); j++) {
				if(tiles[i,j] != null)
					tiles[i,j].gameObject.transform.localPosition = board.CoordToPosition(i, j);
			}
		}
    }

	public void RotateCounterClockwise() {
	    Tile[,] ret = new Tile[cols, row];

	    for (int i = 0; i < cols; ++i) {
	        for (int j = 0; j < row; ++j) {
	            ret[i, j] = tiles[row - j - 1, i];
	        }
	    }

	    tiles = ret;
	    row = tiles.GetLength(0);
	    cols = tiles.GetLength(1);

	    UpdatePosition();
	}

	public void RotateClockwise() {
		Tile[,] ret = new Tile[cols, row];

	    for (int i = 0; i < cols; ++i) {
	        for (int j = 0; j < row; ++j) {
	            ret[i, j] = tiles[j, cols - i - 1];
	        }
	    }

	    tiles = ret;
	    row = tiles.GetLength(0);
	    cols = tiles.GetLength(1);

	    UpdatePosition();
	}
}
