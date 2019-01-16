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
			return Vector2.left;
		} else if (dir == Direction.RIGHT) {
			return Vector2.up;
		} else if (dir == Direction.DOWN) {
			return Vector2.right;
		} else if (dir == Direction.LEFT) {
			return Vector2.down;
		} else {
			return Vector2.zero;
		}
	}

	public static Direction GetOppositeDirection(Direction dir)
	{
		return (Direction)(((int)dir + 2) % 4);
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