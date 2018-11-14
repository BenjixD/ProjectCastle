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

public abstract class Action : MonoBehaviour {
    public string actionName;
	public int cost;
	public List<Frame> frames { get; set; }
}