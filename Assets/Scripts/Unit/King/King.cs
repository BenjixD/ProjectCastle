using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Unit {

	public override bool ConsumeAp(int val) {
		if(val <= curAp) {
			curAp -= val;
			return true;
		}
		return false;
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