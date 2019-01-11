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

	public override Command GetCommand() {
		Command command = new Command();
		command.dir = this.dir;
		command.frame = action.frames[0];
		command.type = ActionType.Effect;

		return command;
	}
}
