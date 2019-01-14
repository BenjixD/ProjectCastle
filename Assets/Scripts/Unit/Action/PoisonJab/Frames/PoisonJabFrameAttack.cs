using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonJabFrameAttack : Frame {

    public const int DMG = 40;
	public const int POISON_DURATION = 3;

    public PoisonJabFrameAttack(Action instance) : base(instance) {}

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
		// Add tiles in range to hit lists
		List<Tile> sweetSpots = new List<Tile>();
		List<Tile> okSpots = new List<Tile>();
		// Sweetspot: 1st tile in front of user
		if (board.CheckCoord(sim.result + frontVect)) {
			sweetSpots.Add(board.GetTile(sim.result + frontVect));
		}
		// Ok spot: 2nd tile in front of user
		if (board.CheckCoord(sim.result + frontVect * 2))
		{
			okSpots.Add(board.GetTile(sim.result + frontVect * 2));
		}
		// Apply effects to units on top of tiles
		foreach (Tile t in sweetSpots) {
			if (t.unit && !IsAlreadyHit(t.unit))
			{
				// Deal damage (40) and apply poison status effect
				t.unit.TakeDamage(DMG);
				t.unit.statusController.QueueAddStatus(new PoisonEffect(POISON_DURATION));
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just took " + DMG + " damage and got poisoned for " + POISON_DURATION + " frames!");
			}
		}
		foreach (Tile t in okSpots)
		{
			if (t.unit && !IsAlreadyHit(t.unit))
			{
				// Deal damage (40)
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
		PoisonJab parentAction = (PoisonJab)actionInstance;
		return parentAction.victims.Contains(unit); 
	}

	private void AddUnitHit(Unit unit) {
		PoisonJab parentAction = (PoisonJab)actionInstance;
		parentAction.victims.Add(unit);
	}

	// TODO move elsewhere
	private Vector2 GetDirectionVector(Direction startDir, Direction relativeDir) {
		Direction absoluteDir = (Direction)(((int)startDir + (int)relativeDir) % 4);
		return DirectionToVector(absoluteDir);
	}

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
}
