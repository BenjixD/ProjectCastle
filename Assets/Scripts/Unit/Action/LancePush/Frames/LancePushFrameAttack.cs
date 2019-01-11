using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePushFrameAttack : Frame {

    public const int DMG = 30;

	public LancePushFrameAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon stun or silence
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO deal damage to enemies on tiles
		Unit user = sim.displacement.unit;
		Vector2 frontVect;
		if ((frontVect = GetDirectionVector(relativeDir, dir)) == Vector2.zero) {
			// bad direction input
			return false;
		}
		List<Tile> hitSpots = new List<Tile>();
		// 1st hitspot: one tile in front of user
		if (board.CheckCoord(sim.result + frontVect)) {
			hitSpots.Add(board.GetTile(sim.result + frontVect));
		}
		// 2nd hitspot: two tiles in front of user
		if (board.CheckCoord(sim.result + frontVect + frontVect))
		{
			hitSpots.Add(board.GetTile(sim.result + frontVect * 2));
		}
		// Apply effects to units on top of tiles
		foreach (Tile t in hitSpots) {
			if (t.unit && !IsAlreadyHit(t.unit))
			{
				// Deal damage (30) and apply knockback status effect
				t.unit.TakeDamage(DMG);
				t.unit.statusController.QueueAddStatus(new KnockbackEffect(dir));
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just got knocked back and took " + DMG + " damage!");
			}
		}
		return true;
	}
	
	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
        // TODO attack anims
        return true;
	}

	private bool IsAlreadyHit(Unit unit) {
        LancePush parentAction = (LancePush)actionInstance;
        return parentAction.victims.Contains(unit); 
    }

    private void AddUnitHit(Unit unit) {
		LancePush parentAction = (LancePush)actionInstance;
        parentAction.victims.Add(unit);
}

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
