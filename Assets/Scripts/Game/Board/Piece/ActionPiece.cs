using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPiece : Piece {

	public Direction dir;
	public Vector2 absoluteOrigin { get; set; }
	public Vector2 localOrigin;

	public void ChangeDirection(Direction newDir)
	{
		int directionsNum = (int)System.Enum.GetValues(typeof(Direction)).Length - 1;
		while (dir != newDir)
		{
			RotateClockwiseAboutOrigin();
			dir++;
			dir = (Direction)((int)dir % directionsNum);
		}
	}

	protected override void UpdatePosition()
	{
		//Move tiles to their corresponding locations
		for (int i = 0; i < tiles.GetLength(0); i++)
		{
			for (int j = 0; j < tiles.GetLength(1); j++)
			{
				if (tiles[i, j] != null)
					tiles[i, j].gameObject.transform.localPosition = board.CoordToPosition(i, j);
			}
		}
		transform.position = board.CoordToPosition((int)(absoluteOrigin.x - localOrigin.x), (int)(absoluteOrigin.y - localOrigin.y));
	}

	public void RotateClockwiseAboutOrigin()
	{
		Tile[,] ret = new Tile[cols, row];

		for (int i = 0; i < cols; ++i)
		{
			for (int j = 0; j < row; ++j)
			{
				ret[i, j] = tiles[row - j - 1, i];
			}
		}

		tiles = ret;
		row = tiles.GetLength(0);
		cols = tiles.GetLength(1);
		localOrigin = new Vector2(localOrigin.y, cols - localOrigin.x - 1);
		
		UpdatePosition();
	}
}
