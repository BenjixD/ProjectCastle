using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour {
	public string unitName;

	public int maxHp;
	public int curHp;

	public int maxAp;
	//public int curAp;

	public bool controllable;
	public int frameUsage;

	public List<Action> skills;

	public Tile tile { get; set; }
	public Player owner { get; set; }

	public Plan plan { get; private set; }
	public StatusController statusController { get; private set; }

	void Awake() {
		plan = new Plan();
		statusController = new StatusController();
	}

	public virtual bool CanConsumeAp(Action action) {
		return action.cost <= maxAp - plan.GetApCost();
	}

	public virtual bool CanConsumeAp(int cost) {
		return cost <= maxAp - plan.GetApCost();	
	}

	public virtual bool CanUseFrame(Action action, Timeline timeline) {
		return plan.GetRemainingFramesCount() + action.frames.Count <= timeline.maxFrame;
	}

	public virtual bool CanUseFrame(int frames, Timeline timeline) {
		return plan.GetRemainingFramesCount() + frames <= timeline.maxFrame;
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
			plan.QueueAction(action, dir, timeline);
			return true;
		}

		Debug.Log("This unit cannot queue anymore actions!");
		return false;
	}

	public void FlushPlan() {
		plan.Flush();
	}		
}
