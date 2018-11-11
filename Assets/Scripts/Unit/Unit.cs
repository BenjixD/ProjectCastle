using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {
	public int maxHp;
	public int curHp;

	public int MaxAp;
	public int curAp;

	public string unitName;
	public List<Action> skills;
	public Queue<Command> plan = new Queue<Command>(); 
	//public Player owner;
	//public Tile tile;

	public abstract bool ConsumeAp(int val); 
	
	public void FlushPlan() {
		plan.Clear();
	}

	public bool QueueAction(Action action) {
		if(this.ConsumeAp(action.cost)) {
			List<Frame> frames = action.frames;
			foreach(Frame frame in frames) {
				Command c = new Command();
				c.frame = frame;
				c.dir = action.dir;
				plan.Enqueue(c);
			}
			return true;
		}

		return false;
	}
}