using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : StatusEffect {
	public StunEffect(int duration = 0) {
		this.duration = duration;
		effectType = StatusEffectType.CONDITION;
		action = new Stun(null);
	}
}
