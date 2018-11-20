using UnityEngine;
using System.Collections;

public abstract class Frame {
	public Direction relativeDir;
	
	public abstract bool ExecuteEffect(SimulatedDisplacement sim, Direction dir, Board board);
	public abstract bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board);
	public abstract bool CanExecute(Unit unit, Direction dir, Board board);

	public virtual UnitDisplacement GetDisplacement(Unit unit, Direction dir, Board board) {
		return new AbsoluteDisplacement(unit, unit.tile.coordinate);
	}
	public GameObject icon { get; set; }
}