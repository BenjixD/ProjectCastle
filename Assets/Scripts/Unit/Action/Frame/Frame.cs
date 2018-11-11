using UnityEngine;
using System.Collections;

public enum Direction {
	NONE,
	UP,
	DOWN,
	LEFT,
	RIGHT
}

public abstract class Frame : MonoBehaviour {
	public Direction relativeDir;
	public abstract bool Execute(Unit unit, Direction dir);
}