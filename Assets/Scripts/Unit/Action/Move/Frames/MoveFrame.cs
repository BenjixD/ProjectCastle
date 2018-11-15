using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveFrame : Frame {
	public override bool Execute(Unit unit, Direction dir, Board board) {
		Direction absoluteDir = (Direction)(((int)relativeDir + (int)dir) % 4);
		Vector2 next = nextCoord(unit.tile.coordinate, absoluteDir);

		if(board.CheckCoord((int)next.x, (int)next.y)) {
            unit.tile.RemoveUnit(unit);
            board.GetTile((int)next.x, (int)next.y).PlaceUnit(unit);
			unit.gameObject.transform.position = unit.tile.gameObject.transform.position;	//TODO: Should be some sort of slow walk to the tile
			return true;
		}

		return false;
	}

	private Vector2 nextCoord(Vector2 coord, Direction dir) {
		if(dir == Direction.UP) {
			return new Vector2(coord.x - 1, coord.y);
		} else if (dir == Direction.RIGHT) {
			return new Vector2(coord.x, coord.y + 1);
		} else if (dir == Direction.DOWN) {
			return new Vector2(coord.x + 1, coord.y);
		} else if (dir == Direction.LEFT) {
			return new Vector2(coord.x, coord.y - 1);
		} else {
			return coord;
		}
	} 
}