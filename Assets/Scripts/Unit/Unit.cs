using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {
	public int maxHp;
	public int curHp;

	public int MaxAp;
	public int curAp;

	public string unitName;
	public List<Action> actions;
	//public Player owner;
	//public Tile tile;

	public abstract bool ConsumeAp(int val); 
}