using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class RelativeDisplacement : UnitDisplacement {
	Vector2 displacement;

	public static RelativeDisplacement operator+ (RelativeDisplacement a, RelativeDisplacement b) {
		if(a.unit != b.unit) {
			throw new Exception("Units are not the same");
		}

		return new RelativeDisplacement(a.unit, a.displacement + b.displacement);
	}

	public RelativeDisplacement(Unit unit, Vector2 displacement) : base(unit) {
		this.type = UnitDisplacementType.RELATIVE;
		this.displacement = displacement;
	}

	public Vector2 GetUnitStep() {
		if(displacement.Equals(new Vector2(0, 0))) {
			if(Mathf.Abs(displacement.x) >= Mathf.Abs(displacement.y)) {
				return displacement / displacement.x;
			} else {
				return displacement / displacement.y;
			}
		} else {
			return displacement;
		}
	}

	public override Vector2 GetTargetCoordinate() {
		return GetStartCoordinate() + displacement;
	}
}