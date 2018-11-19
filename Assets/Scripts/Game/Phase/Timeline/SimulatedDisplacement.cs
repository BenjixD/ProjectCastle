using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimulatedDisplacement {
	public UnitDisplacement displacement;
	public Vector2 result;
	public Vector2 current;
	public bool conflict;

	public SimulatedDisplacement(UnitDisplacement displacement, Vector2 target) {
		this.displacement = displacement;
		this.result = target;
		this.current = displacement.GetStartCoordinate();
		this.conflict = false;
	}

	public Vector2 GetNextSimulationStep() {
		if(displacement.type == UnitDisplacementType.RELATIVE && !CheckCoordinateEquality(current, result)) {
			RelativeDisplacement rd = (RelativeDisplacement)displacement;
			current += rd.GetUnitStep();
			current = new Vector2(Mathf.Round(current.x), Mathf.Round(current.y));
		} else if(displacement.type == UnitDisplacementType.ABSOLUTE){
			current = result;
		}
		return current;
	}

	public Vector2 GetPreviousSimulationStep() {
		if(displacement.type == UnitDisplacementType.RELATIVE && !CheckCoordinateEquality(current, displacement.GetStartCoordinate())) {
			RelativeDisplacement rd = (RelativeDisplacement)displacement;
			current -= rd.GetUnitStep();
			current = new Vector2(Mathf.Round(current.x), Mathf.Round(current.y));
		} else if(displacement.type == UnitDisplacementType.ABSOLUTE){
			current = displacement.GetStartCoordinate();
		}
		return current;
	}

	public void DisplaceUnit(Board board) {
		Unit unit = displacement.unit;
		unit.tile = board.GetTile(new Vector2(Mathf.Round(current.x), Mathf.Round(current.y)));
	}

	static private bool CheckCoordinateEquality(Vector2 a, Vector2 b) {
		return Math.Round(a.x) == Math.Round(b.x) && Math.Round(a.y) == Math.Round(b.y);
	}
}