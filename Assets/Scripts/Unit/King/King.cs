using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Unit {

	public override bool CanConsumeAp(Action action) {
		return action.cost <= curAp;
	}

	public override void ConsumeAp(Action action) {
		curAp -= action.cost;
	}

	public override bool IsDead() {
		return curHp <= 0;
	}

	public override void TakeDamage(int val) {
		curHp -= val;
	}

	public override void RefreshAp(){
		curAp = maxAp;
	}
}