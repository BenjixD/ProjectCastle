using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FrameEffectType {
	Skill,
	Movement
}

public enum HitboxType {
	SWEET,
	OK,
	SOUR
}

public abstract class FrameEffect {
	public Action action;
	public HashSet<FrameEffectType> frameEffectTypes;
	
	public FrameEffect(Action instance) {
		this.action = instance;
	}

	public GameObject icon { get; set; } //TODO: Move this to descriptor

	public abstract bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board);
	public abstract bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board);

	public virtual bool IsInterrupted(StatusController statusController) {
		foreach (KeyValuePair<string, StatusEffect> pair in statusController) {
			StatusEffect status = pair.Value;

			//TODO: FIX THIS
			if(status.GetType().Name == "Stun") {
				if(frameEffectTypes.Contains(FrameEffectType.Skill) || frameEffectTypes.Contains(FrameEffectType.Movement)) {
					return true;
				}
			}
		}

		return false;
	}

	public virtual UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		return new AbsoluteDisplacement(unit, unit.tile.coordinate);
	}
}