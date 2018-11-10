using UnityEngine;
using System.Collections;

public enum Direction {
	NONE,
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public abstract class Action : MonoBehaviour {
	public int cost;
	public int frames;

	protected abstract bool ConsumeAp(Unit unit);
	protected abstract bool Act(Unit unit, Direction dir);
	public abstract bool Execute(Unit unit, Direction dir);
}