using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {
	public string unitName;

	public int maxHp;
	public int curHp;

	public int maxAp;
	public int curAp;

	public bool controllable;

	public List<Action> skills;
	public Queue<Command> plan = new Queue<Command>(); 

	public Tile tile { get; set; }
	public Player owner { get; set; }

	public abstract void RefreshAp();
	public abstract bool ConsumeAp(int val); 
	
	public abstract void TakeDamage(int val);
	public abstract bool IsDead();

	public void FlushPlan() {
		plan.Clear();
	}

	public bool QueueAction(Action action, Direction dir) {
		if(this.ConsumeAp(action.cost)) {
			List<Frame> frames = action.frames;
			foreach(Frame frame in frames) {
				Command c = new Command();
				c.frame = frame;
				c.dir = dir;
				c.type = action.actionType;
				plan.Enqueue(c);
			}
			return true;
		}

		Debug.Log("This unit cannot queue anymore actions!");
		return false;
	}
}