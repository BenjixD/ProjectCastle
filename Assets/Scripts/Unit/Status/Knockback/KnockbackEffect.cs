using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEffect : StatusEffect {
	public KnockbackEffect(Vector2 vector) {
		duration = 1;
		effectType = StatusEffectType.CONDITION;
		action = new Knockback(vector, null);
		this.dir = Direction.NONE;
	}

	//Accumulate Knockback Vector
	public override StatusEffect GetOverwrite(StatusEffect other) {
		KnockbackEffect kbOther = ((KnockbackEffect)other);
		((Knockback)this.action).vector += ((Knockback)kbOther.action).vector;
		return this;
	}

	//No need to override GetCommand
	//public override Command GetCommand(Direction dir);
	//public override Command GetCommand();	
}
