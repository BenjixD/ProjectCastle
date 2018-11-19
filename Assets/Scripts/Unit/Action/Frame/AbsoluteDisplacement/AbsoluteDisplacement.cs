using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class AbsoluteDisplacement : UnitDisplacement {
	Vector2 coordinate;

	//AbsoluteDisplacement have priority over RelativeDisplacement(ie Blink overrides Dash)
	public static AbsoluteDisplacement operator+ (AbsoluteDisplacement a, RelativeDisplacement b) {
		if(a.unit != b.unit) {
			throw new Exception("Units are not the same");
		}

		return a;
	}

	public static AbsoluteDisplacement operator+ (RelativeDisplacement a, AbsoluteDisplacement b) {
		if(a.unit != b.unit) {
			throw new Exception("Units are not the same");
		}

		return b;
	}

	public AbsoluteDisplacement(Unit unit, Vector2 coordinate) : base(unit) {
		this.type = UnitDisplacementType.ABSOLUTE;
		this.coordinate = coordinate;
	}

	public override Vector2 GetTargetCoordinate() {
		return coordinate;
	}

}