using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePokeFrameAttack : Frame {

    public const int SOURSPOTDMG = 30;
    public const int OKSPOTDMG = 40;
    public const int SWEETSPOTDMG = 50;

    public LancePokeFrameAttack(Action instance) : base(instance) {}

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon stun or silence
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
        // TODO deal damage to enemies on tiles
        Unit user = sim.displacement.unit;
        Vector2 attackDir;
        if ((attackDir = GetDirectionVector(user, dir, board)) == Vector2.zero){
            // bad direction input
            return false;
        }
        // Add tiles in range to hit lists
        List<Tile> sourSpots = new List<Tile>();
        List<Tile> okSpots = new List<Tile>();
        List<Tile> sweetSpots = new List<Tile>();
        // Sourspot: 1st tile from user
        if (board.CheckCoord(sim.result + attackDir)) {
            sourSpots.Add(board.GetTile(sim.result + attackDir));
        }
        // Ok spot: 2nd tile from user
        if (board.CheckCoord(sim.result + attackDir * 2)) {
            okSpots.Add(board.GetTile(sim.result + attackDir * 2));
        }
        // Sweetspot: 3rd tile from user
        if (board.CheckCoord(sim.result + attackDir * 3)) {
            sweetSpots.Add(board.GetTile(sim.result + attackDir * 3));
        }

        // Deal damage to units on top of tiles
        foreach (Tile t in sourSpots) {
            if (t.unit && !IsAlreadyHit(t.unit))
            {
                // Deal sourspot damage (30)
                t.unit.TakeDamage(SOURSPOTDMG);
                AddUnitHit(t.unit);
                Debug.Log("Ouch! " + t.unit.unitName + " just took " + SOURSPOTDMG + " damage!");
            }
        }
        foreach (Tile t in okSpots)
        {
            if (t.unit && !IsAlreadyHit(t.unit))
            {
                // Deal okspot damage (40)
                t.unit.TakeDamage(OKSPOTDMG);
                AddUnitHit(t.unit);
                Debug.Log("Ouch! " + t.unit.unitName + " just took " + OKSPOTDMG + " damage!");
            }
        }
        foreach (Tile t in sweetSpots)
        {
            if (t.unit && !IsAlreadyHit(t.unit))
            {
                // Deal sweetspot damage (50)
                t.unit.TakeDamage(SWEETSPOTDMG);
                AddUnitHit(t.unit);
                Debug.Log("Ouch! " + t.unit.unitName + " just took " + SWEETSPOTDMG + " damage!");
            }
        }
        return true;
	}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
        // TODO attack anims
        return true;
	}

    private bool IsAlreadyHit(Unit unit) {
        LancePoke parentAction = (LancePoke)actionInstance;
        return parentAction.victims.Contains(unit); 
    }

    private void AddUnitHit(Unit unit) {
        LancePoke parentAction = (LancePoke)actionInstance;
        parentAction.victims.Add(unit);
    }

	private Vector2 GetDirectionVector(Unit unit, Direction dir, Board board) {
        // Keep this for attack direction
		Direction absoluteDir = (Direction)(((int)relativeDir + (int)dir) % 4);

		if(absoluteDir == Direction.UP) {
			return new Vector2(-1, 0);
		} else if (absoluteDir == Direction.RIGHT) {
			return new Vector2(0, 1);
		} else if (absoluteDir == Direction.DOWN) {
			return new Vector2(1, 0);
		} else if (absoluteDir == Direction.LEFT) {
			return new Vector2(0, -1);
		} else {
			return new Vector2(0, 0);
		}
	}
}