using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashFrameEffectAttack : FrameEffect {

	public const int DMG = 60;

	public SwordSlashFrameEffectAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon silence
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect(0))) {
			return false;
		}
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO deal damage to enemies on tiles
		Vector2 frontVect = Action.GetDirectionVector(dir);
		Vector2 leftVect = ComputeAbsoluteVector(dir, Direction.LEFT);
		Vector2 rightVect = ComputeAbsoluteVector(dir, Direction.RIGHT);

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

	private bool IsAlreadyHit(Unit unit) {
		SwordSlash parentAction = (SwordSlash)action;
		return parentAction.victims.Contains(unit); 
	}

	private void AddUnitHit(Unit unit) {
		SwordSlash parentAction = (SwordSlash)action;
		parentAction.victims.Add(unit);
	}

	private Vector2 ComputeAbsoluteVector(Direction startDir, Direction relativeDir) {
		Direction absoluteDir = (Direction)(((int)startDir + (int)relativeDir) % 4);
		return Action.GetDirectionVector(absoluteDir);
	}
}
