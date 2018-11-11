using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Action : MonoBehaviour {
	public int cost;
	public Direction dir;
	public List<Frame> frames = new List<Frame>();
}