using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinEffect : KnockbackEffect {
	public PinEffect(Vector2 vector) : base(vector) {
		duration = 1;
		effectType = StatusEffectType.CONDITION;
		action = new Pin(vector, null);
		this.dir = Direction.NONE;
	}
}
