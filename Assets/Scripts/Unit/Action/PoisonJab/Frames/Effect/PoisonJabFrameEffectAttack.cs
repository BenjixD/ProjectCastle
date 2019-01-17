using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonJabFrameEffectAttack : FrameEffect {

	public const int DMG = 40;
	public const int POISON_DURATION = 3;

	public PoisonJabFrameEffectAttack(Action instance) : base(instance) {}

	public override bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board) {
		// TODO Fail upon silence
		if (sim.displacement.unit.statusController.HasStatus(new StunEffect(0))) {
			return false;
		}
		return true;
	}

	public override bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board) {
		Vector2 frontVect = Action.GetDirectionVector(dir);
		if (frontVect == Vector2.zero) {
			// bad direction input
			return false;
		}
		List<Tile> sweetSpots = new List<Tile>();
		List<Tile> okSpots = new List<Tile>();
		// Sweetspot: 1st tile in front of user
		if (board.CheckCoord(sim.result + frontVect)) {
			sweetSpots.Add(board.GetTile(sim.result + frontVect));
		}
		// Okspot: 2nd tile in front of user
		if (board.CheckCoord(sim.result + frontVect * 2)) {
			okSpots.Add(board.GetTile(sim.result + frontVect * 2));
		}
		// Apply effects to units on top of tiles
		foreach (Tile t in sweetSpots) {
			if (t.unit && !IsAlreadyHit(t.unit)) {
				// Deal damage (40) and apply poison status effect
				t.unit.TakeDamage(DMG);
				t.unit.statusController.QueueAddStatus(new PoisonEffect(POISON_DURATION));
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just took " + DMG + " damage and got poisoned for " + POISON_DURATION + " frames!");
			}
		}
		foreach (Tile t in okSpots) {
			if (t.unit && !IsAlreadyHit(t.unit)) {
				// Deal damage (40)
				t.unit.TakeDamage(DMG);
				t.unit.statusController.QueueAddStatus(new KnockbackEffect(frontVect));
				AddUnitHit(t.unit);
				Debug.Log("Ouch! " + t.unit.unitName + " just took " + DMG + " damage!");
			}
		}
		return true;
	}

	private bool IsAlreadyHit(Unit unit) {
		PoisonJab parentAction = (PoisonJab)action;
		return parentAction.victims.Contains(unit); 
	}

	private void AddUnitHit(Unit unit) {
		PoisonJab parentAction = (PoisonJab)action;
		parentAction.victims.Add(unit);
	}
}