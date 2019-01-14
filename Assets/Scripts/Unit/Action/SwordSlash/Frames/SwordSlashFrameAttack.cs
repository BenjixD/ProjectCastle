using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashFrameAttack : Frame {

	public const int DMG = 60;

	public SwordSlashFrameAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon stun or silence
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO deal damage to enemies on tiles
		Vector2 frontVect;
		if ((frontVect = GetDirectionVector(relativeDir, dir)) == Vector2.zero) {
			// bad direction input
			return false;
		}
		Direction frontDir = VectorToDirection(frontVect);
		Vector2 leftVect = GetDirectionVector(frontDir, Direction.LEFT);
		Vector2 rightVect = GetDirectionVector(frontDir, Direction.RIGHT);
		List<Tile> hitSpots = new List<Tile>();
		// 1st hitspot: one tile in front of user
		if (board.CheckCoord(sim.result + frontVect)) {
			hitSpots.Add(board.GetTile(sim.result + frontVect));
		}
		// 2nd and 3rd hitspots: one tile on each side of the first one
		if (board.CheckCoord(sim.result + frontVect + leftVect)) {
			hitSpots.Add(board.GetTile(sim.result + frontVect + leftVect));
		}
		if (board.CheckCoord(sim.result + frontVect + rightVect)) {
			hitSpots.Add(board.GetTile(sim.result + frontVect + rightVect));
		}
		// Deal damage to units on top of tiles
		foreach (Tile t in hitSpots) {
			if (t.unit && !IsAlreadyHit(t.unit))
			{
				// Deal damage (60)
				t.unit.TakeDamage(DMG);
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just took " + DMG + " damage!");
			}
		}
		return true;
	}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO attack anims
		return true;
	}

	private bool IsAlreadyHit(Unit unit) {
		SwordSlash parentAction = (SwordSlash)actionInstance;
		return parentAction.victims.Contains(unit); 
	}

	private void AddUnitHit(Unit unit) {
		SwordSlash parentAction = (SwordSlash)actionInstance;
		parentAction.victims.Add(unit);
	}

	private Vector2 GetDirectionVector(Direction startDir, Direction relativeDir) {
		Direction absoluteDir = (Direction)(((int)startDir + (int)relativeDir) % 4);
		return DirectionToVector(absoluteDir);
	}

	// TODO move elsewhere
	private Vector2 DirectionToVector(Direction dir)
	{
		if (dir == Direction.UP) {
			return new Vector2(-1, 0);
		} else if (dir == Direction.RIGHT) {
			return new Vector2(0, 1);
		} else if (dir == Direction.DOWN) {
			return new Vector2(1, 0);
		} else if (dir == Direction.LEFT) {
			return new Vector2(0, -1);
		} else {
			return new Vector2(0, 0);
		}
	}

	private Direction VectorToDirection(Vector2 v)
	{
		if (v == new Vector2(-1, 0)) {
			return Direction.UP;
		} else if (v == new Vector2(0, 1)) {
			return Direction.RIGHT;
		} else if (v == new Vector2(1, 0)) {
			return Direction.DOWN;
		} else if (v == new Vector2(0, -1)) {
			return Direction.LEFT;
		} else {
			return Direction.NONE;
		}
	}
}
