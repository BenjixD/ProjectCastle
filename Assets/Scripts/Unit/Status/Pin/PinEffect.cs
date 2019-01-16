using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinEffect : KnockbackEffect {
	public PinEffect(Direction dir) : base(dir) {
		duration = 1;
		effectType = StatusEffectType.CONDITION;
		action = new Pin(null);
		this.dir = dir;
	}
}
