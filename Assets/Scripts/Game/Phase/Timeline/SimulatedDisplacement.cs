using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimulatedDisplacement {
	public UnitDisplacement displacement;
	public Vector2 result;
	public bool conflict;
	public bool outOfBounds;

	private Vector2 current;

	public SimulatedDisplacement(UnitDisplacement displacement) {
		this.displacement = displacement;
		this.result = displacement.GetTargetCoordinate();
		this.current = displacement.GetStartCoordinate();
		this.conflict = false;
		this.outOfBounds = false;
	}

	public Vector2 GetNextSimulationStep(Board board) {
		if(displacement.type == UnitDisplacementType.RELATIVE && !CheckCoordinateEquality(current, result)) {
			RelativeDisplacement rd = (RelativeDisplacement)displacement;
			Vector2 next = current + rd.GetUnitStep();
			if(board.CheckCoord(new Vector2(Mathf.Round(next.x), Mathf.Round(next.y)))) {
				current = next;
			} else {
				outOfBounds = true;
			}
		} else if(displacement.type == UnitDisplacementType.ABSOLUTE){
			current = result;
		}
		Debug.Log(current);
		return GetCurrentVector();
	}

	public Vector2 GetPreviousSimulationStep(Board board) {
		if(displacement.type == UnitDisplacementType.RELATIVE && !CheckCoordinateEquality(current, displacement.GetStartCoordinate())) {
			RelativeDisplacement rd = (RelativeDisplacement)displacement;
			Vector2 prev = current - rd.GetUnitStep();
			if(board.CheckCoord(new Vector2(Mathf.Round(prev.x), Mathf.Round(prev.y)))) {
				current = prev;
			} else {
				outOfBounds = true;
			}
		} else if(displacement.type == UnitDisplacementType.ABSOLUTE){
			current = displacement.GetStartCoordinate();
		}
		return GetCurrentVector();
	}

	public Vector2 GetCurrentVector() {
		return new Vector2(Mathf.Round(current.x), Mathf.Round(current.y));
	}

	public Unit GetUnit() {
		return displacement.unit;
	}

	static private bool CheckCoordinateEquality(Vector2 a, Vector2 b) {
		return Mathf.Round(a.x) == Mathf.Round(b.x) && Mathf.Round(a.y) == Mathf.Round(b.y);
	}
}