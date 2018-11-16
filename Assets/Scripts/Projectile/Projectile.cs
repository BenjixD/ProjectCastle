using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Projectile : MonoBehaviour {
	public string projectileName;
	public Unit owner;
	public List<Action> skills;
	public Queue<Command> plan = new Queue<Command>(); 
	public Tile tile { get; set; }

	public abstract bool QueueAction(Action action, Direction dir);
	public abstract void OnImpact(List<Unit> units);
}