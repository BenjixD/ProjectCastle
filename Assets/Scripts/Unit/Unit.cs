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
	public int frameUsage;

	public List<Action> skills;
	public Queue<Command> plan = new Queue<Command>(); 

	public Tile tile { get; set; }
	public Player owner { get; set; }


	// AP Related Methods
	public virtual void RefreshAp() {
		curAp = maxAp;
	}

	public virtual bool CanConsumeAp(Action action) {
		return action.cost <= curAp;
	}

	public virtual bool CanConsumeAp(int cost) {
		return cost <= curAp;	
	}

	public virtual void ConsumeAp(Action action) {
		curAp -= action.cost;
	}

	// Frame related methods
	public virtual void ResetFrameUsage() {
		frameUsage = 0;
	}

	public virtual bool CanUseFrame(Action action, Timeline timeline) {
		return action.frames.Count + frameUsage <= timeline.maxFrame;
	}

	public virtual bool CanUseFrame(int frames, Timeline timeline) {
		return frames + frameUsage <= timeline.maxFrame;	
	}

	public virtual void UseFrame(Action action, Timeline timeline) {
		frameUsage += action.frames.Count;
	}


	// Damage Related Methods
	public virtual void TakeDamage(int val) {
		curHp -= val;
	}
	
	public virtual bool IsDead() {
		return curHp <= 0;
	}

	// Queue Action method
	public bool QueueAction(Action action, Direction dir, Timeline timeline) {
		if(this.CanConsumeAp(action) && this.CanUseFrame(action, timeline)) {
			//Consume Costs
			this.ConsumeAp(action);
			this.UseFrame(action, timeline);
			//Add to queue
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

	//FLush Plan helper
	public void FlushPlan() {
		plan.Clear();
	}
}
