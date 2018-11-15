using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {
	public string unitName;

	public int maxHp;
	public int curHp;

	public int maxAp;
	public int curAp;

	public int frameUsage;

	public List<Action> skills;
	public Queue<Command> plan = new Queue<Command>(); 

	public Tile tile { get; set; }
	public Player owner { get; set; }

	public abstract void RefreshAp();
	public abstract bool CanConsumeAp(Action action);
	public abstract void ConsumeAp(Action action);
	
	public abstract void TakeDamage(int val);
	public abstract bool IsDead();

	public void FlushPlan() {
		plan.Clear();
	}

	public void ResetFrameUsage() {
		frameUsage = 0;
	}

	public bool CanUseFrame(Action action, Timeline timeline) {
		return action.frames.Count + frameUsage <= timeline.maxFrame;
	}
	public void UseFrame(Action action, Timeline timeline) {
		frameUsage += action.frames.Count;
	}

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
}