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

public abstract class Action : MonoBehaviour {
	public InputManager inputManager;
	public string actionName;
	public int cost;
	public List<Frame> frames { get; set; }
	public GameObject[] icons;
	public ActionType actionType;
	public abstract void Select(Unit unit);
}