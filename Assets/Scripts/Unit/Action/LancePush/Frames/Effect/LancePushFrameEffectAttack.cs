using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePushFrameEffectAttack : FrameEffect {

    public const int DMG = 30;

	public LancePushFrameEffectAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon stun or silence
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO deal damage to enemies on tiles
		Vector2 frontVect = Action.GetDirectionVector(dir);
		if (frontVect == Vector2.zero) {
			// bad direction input
			return false;
		}
		List<Tile> hitSpots = new List<Tile>();
		// 1st hitspot: one tile in front of user
		if (board.CheckCoord(sim.result + frontVect)) {
			hitSpots.Add(board.GetTile(sim.result + frontVect));
		}
		// 2nd hitspot: two tiles in front of user
		if (board.CheckCoord(sim.result + frontVect * 2))
		{
			hitSpots.Add(board.GetTile(sim.result + frontVect * 2));
		}
		// Apply effects to units on top of tiles
		foreach (Tile t in hitSpots) {
			if (t.unit && !IsAlreadyHit(t.unit))
			{
				// Deal damage (30) and apply knockback status effect
				t.unit.TakeDamage(DMG);
				t.unit.statusController.QueueAddStatus(new KnockbackEffect(frontVect));
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just got knocked back and took " + DMG + " damage!");
			}
		}
		return true;
	}

	private bool IsAlreadyHit(Unit unit) {
		LancePush parentAction = (LancePush)action;
		return parentAction.victims.Contains(unit); 
	}

	private void AddUnitHit(Unit unit) {
		LancePush parentAction = (LancePush)action;
		parentAction.victims.Add(unit);
	}
}
