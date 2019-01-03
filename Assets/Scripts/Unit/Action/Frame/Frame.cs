using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FrameType {
	Skill,
	Movement
}

public abstract class Frame {
	public Direction relativeDir;
	public HashSet<FrameType> frameTypes;
	
	public abstract bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board);
	public abstract bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board);
	public abstract bool CanExecute(SimulatedDisplacement sim, Direction dir, Board board);

	public virtual bool IsInterrupted(StatusController statusController) {
		foreach (KeyValuePair<string, StatusEffect> pair in statusController) {
			StatusEffect status = pair.Value;

			//TODO: FIX THIS
			if(status.GetType().Name == "Stun") {
				if(frameTypes.Contains(FrameType.Skill) || frameTypes.Contains(FrameType.Movement)) {
					return true;
				}
			}
		}

		return false;
	}

	public virtual UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		return new AbsoluteDisplacement(unit, unit.tile.coordinate);
	}

	public GameObject icon { get; set; }
}