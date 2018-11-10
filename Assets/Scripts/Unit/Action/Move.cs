using UnityEngine;
using System.Collections;

public class Move : Action {
	//Consume AP from unit
	protected override bool ConsumeAp(Unit unit) {
		return unit.ConsumeAp(cost);
	}

	//Move the unit in direction
	protected override bool Act(Unit unit, Direction dir) {
		return false;
	}

	public override bool Execute(Unit unit, Direction dir) {
		if (!ConsumeAp(unit))
			return false;

		return Act(unit, dir);
	}
}