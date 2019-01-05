using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : StatusEffect {
	public KnockbackEffect()
	{
		duration = 1;
		effectType = StatusEffectType.CONDITION;
		action = new Knockback();
	}
}
