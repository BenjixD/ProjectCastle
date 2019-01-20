using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : StatusEffect {
	public PoisonEffect(int duration) {
		this.duration = duration;
		effectType = StatusEffectType.CONDITION;
		action = new Poison(null);
		dir = Direction.NONE;
	}
}
