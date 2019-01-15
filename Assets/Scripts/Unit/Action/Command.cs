using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Command {
	public Frame frame;
	public Direction dir { private get; set; }
	public ActionType type;

	public Direction GetRelativeDir() {
		return dir;
	}

	public Direction GetAbsoluteDir() {
		return frame.effect.action.GetAbsoluteDir(dir);
	}
}