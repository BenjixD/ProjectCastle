using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worker : Unit {
	public override bool ConsumeAp(int val) {
		if (val > curAp) {
			return false;
		}
		else {
			curAp -= val;
		}
		return true;
	}
}