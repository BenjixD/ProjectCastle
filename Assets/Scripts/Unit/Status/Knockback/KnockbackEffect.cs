using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : StatusEffect {
	public KnockbackEffect(Direction dir) {
		duration = 1;
		effectType = StatusEffectType.CONDITION;
		action = new Knockback(null);
		this.dir = dir;
	}

	//No need to override GetCommand
	//public override Command GetCommand(Direction dir);
	//public override Command GetCommand();	
}
