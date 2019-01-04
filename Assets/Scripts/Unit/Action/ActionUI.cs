using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ActionSubmissionState {
	INITIALIZING = -1,
	CANCELLED = 0,
	SUBMITTED = 1,
	ACTIVE = 2
}

public abstract class ActionUI : MonoBehaviour {
	public Timeline timeline;
	public Unit unit;
	public Action action;
    public Board board;
	public ActionSubmissionState state = ActionSubmissionState.INITIALIZING;

	public abstract bool CanAddAction(Action action);

	public abstract void SubmitInput();
	public abstract void CancelInput();

	public virtual void Initialize(Unit unit, Action action, Board board, Timeline timeline) {
        this.board = board;
		this.unit = unit;
		this.action = action;
		this.timeline = timeline;
		state = ActionSubmissionState.ACTIVE;
	}

	public virtual int GetApCost() {
		return action.cost;
	}
	public virtual int GetFrameCost() {
		return action.frames.Count;
	}
}