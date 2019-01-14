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

	public override Command GetCommand(Direction dir)
	{
		Command command = new Command();
		command.dir = dir;
		command.frame = action.frames[0];
		command.type = ActionType.Effect;

		return command;
	}

	public override Command GetCommand() {
		Command command = new Command();
		command.dir = this.dir;
		command.frame = action.frames[0];
		command.type = ActionType.Effect;

		return command;
	}
}
