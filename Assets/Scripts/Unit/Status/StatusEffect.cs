using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum StatusEffectType {
	CONDITION = 0,
	DEBUFF = 1,
	BUFF = 2
}

public abstract class StatusEffect {
	public int duration;
	public StatusEffectType effectType;
	public Action action;
	public Direction dir;

	public virtual StatusEffect GetOverwrite(StatusEffect other) {
		if(other.duration > this.duration) {
			return other;
		} else {
			return this;
		}
	}

	public virtual Command GetCommand(Direction dir) {
		Command command = new Command();
		command.dir = dir;
		command.frame = action.frames[0];
		//command.type = action.descriptor.actionType; //TODO: Give status an action type

		return command;
	}

	public virtual Command GetCommand() {
		Command command = new Command();
		command.dir = this.dir;
		command.frame = action.frames[0];
		//command.type = action.descriptor.actionType; //TODO: Give status an action type

		return command;
	}
}