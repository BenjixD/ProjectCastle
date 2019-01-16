using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePokeFrameEffectAttack : FrameEffect {

    public const int SOURSPOTDMG = 30;
    public const int OKSPOTDMG = 40;
    public const int SWEETSPOTDMG = 50;

    public LancePokeFrameEffectAttack(Action instance) : base(instance) {}

    public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon silence
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect())) {
			return false;
		}
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
        // TODO deal damage to enemies on tiles
        Vector2 attackDir = Action.GetDirectionVector(dir);

        if (attackDir == Vector2.zero){
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

    private bool IsAlreadyHit(Unit unit) {
        LancePoke parentAction = (LancePoke)action;
        return parentAction.victims.Contains(unit); 
    }

    private void AddUnitHit(Unit unit) {
        LancePoke parentAction = (LancePoke)action;
        parentAction.victims.Add(unit);
    }
}