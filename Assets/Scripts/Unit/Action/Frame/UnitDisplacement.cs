using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum UnitDisplacementType {
	ABSOLUTE,
	RELATIVE
}

public abstract class UnitDisplacement {
	public Unit unit;
	public UnitDisplacementType type;
	
	private Vector2 startCoordinate;

	public static UnitDisplacement operator+ (UnitDisplacement a, UnitDisplacement b) {
		if(a.type == UnitDisplacementType.RELATIVE && b.type == UnitDisplacementType.RELATIVE) {
			return (RelativeDisplacement)a + (RelativeDisplacement)b;
		} else if (a.type == UnitDisplacementType.ABSOLUTE && b.type == UnitDisplacementType.RELATIVE) {
			return (AbsoluteDisplacement)a + (RelativeDisplacement)b;
		} else if(a.type == UnitDisplacementType.RELATIVE && b.type == UnitDisplacementType.ABSOLUTE) {
			return (RelativeDisplacement)a + (AbsoluteDisplacement)b;
		}

		throw new Exception("Can't handle summing unit displacements");
	}

	public UnitDisplacement(Unit unit) {
		this.unit = unit;
		startCoordinate = unit.tile.coordinate;
	}

	public virtual Vector2 GetStartCoordinate() {
		return startCoordinate;
	}

	public virtual Vector2 GetTargetCoordinate() {
		return unit.tile.coordinate;
	}
}