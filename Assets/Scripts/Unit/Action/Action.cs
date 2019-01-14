using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {
	NONE = -1,
	UP = 0,
	RIGHT = 1,
	DOWN = 2,
	LEFT = 3
}

public enum ActionType {
	Priority,
	Movement,
	Effect,
	Attack
}

public abstract class Action {
	public List<Frame> frames { get; set; }
	public Frame defaultFrame { get; set; }
	public Direction relativeDir;

	public ActionDescriptor descriptor;

	public Action(ActionDescriptor descriptor) {
		this.descriptor = descriptor;
		this.relativeDir = Direction.UP;
		this.frames = new List<Frame>();
	}

	public static Vector2 GetDirectionVector(Direction dir) {
		if(dir == Direction.UP) {
			return new Vector2(-1, 0);
		} else if (dir == Direction.RIGHT) {
			return new Vector2(0, 1);
		} else if (dir == Direction.DOWN) {
			return new Vector2(1, 0);
		} else if (dir == Direction.LEFT) {
			return new Vector2(0, -1);
		} else {
			return new Vector2(0, 0);
		}
	}

	public Direction GetAbsoluteDir(Direction dir) {
		if(dir == Direction.NONE) {
			return relativeDir;
		} else {
			return (Direction)(((int)relativeDir + (int)dir) % 4);	
		}
	}

	protected void AddFrame(FrameEffect effect, FrameAnim anim) {
		frames.Add(new Frame(effect, anim));
	}

	protected void AddCustomFrame(Frame frame) {
		frames.Add(frame);
	}

	protected void SetDefaultFrame(FrameEffect effect, FrameAnim anim) {
		defaultFrame = new Frame(effect, anim);
	}

	protected void SetCustomDefaultFrame(Frame frame) {
		defaultFrame = frame;
	}
}