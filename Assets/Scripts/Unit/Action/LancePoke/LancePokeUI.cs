using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePokeUI : ActionUI {
	public List<KeyValuePair<Direction, Action>> moves;

	void Start()
	{
		moves = new List<KeyValuePair<Direction, Action>>();
	}

	void Update() {
		if(state == ActionSubmissionState.ACTIVE) {
			if (Input.GetKeyDown("up"))
			{
				Direction dir = Direction.UP;
				QueueInput(dir, action);
			}
			else if (Input.GetKeyDown("down"))
			{
				Direction dir = Direction.DOWN;
				QueueInput(dir, action);
			}
			else if (Input.GetKeyDown("left"))
			{
				Direction dir = Direction.LEFT;
				QueueInput(dir, action);
			}
			else if (Input.GetKeyDown("right"))
			{
				Direction dir = Direction.RIGHT;
				QueueInput(dir, action);
			}
			else if (Input.GetKeyDown("return"))
			{
				SubmitInput();
			}
			else if(Input.GetKeyDown("escape") || Input.GetKeyDown("backspace")) 
			{
				CancelInput();
			}
		}
	}

	public void QueueInput(Direction dir, Action action) {
		if(CanAddAction(action)) {
			moves.Add(new KeyValuePair<Direction, Action>(dir, action));	
		}
	}

	public override void SubmitInput() {
		if(moves.Count > 0) {
			foreach(KeyValuePair<Direction, Action> pair in moves) {
				unit.QueueAction(pair.Value, pair.Key, timeline);
			}
			state = ActionSubmissionState.SUBMITTED;
		} else {
			Debug.Log("Nothing to Submit");
		}
	}

	public override void CancelInput() {
		state = ActionSubmissionState.CANCELLED;
	}

	public override bool CanAddAction(Action action) {
		if(!unit.CanConsumeAp(action.cost + GetApCost())) {
			Debug.Log("Unit cannnot consume anymore AP!");
			return false;
		}
		else if (!unit.CanUseFrame(action.frames.Count + GetFrameCost(), timeline)) {
			Debug.Log("Unit cannot exceed total number of frames!");	
			return false;
		}
		return true;
	}

	public override int GetApCost() {
		int total = 0;
		foreach(KeyValuePair<Direction, Action> pair in moves) {
			total += pair.Value.cost;
		}
		return total;
	}

	public override int GetFrameCost() {
		int total = 0;
		foreach(KeyValuePair<Direction, Action> pair in moves) {
			total += pair.Value.frames.Count;
		}
		return total;
	}
}