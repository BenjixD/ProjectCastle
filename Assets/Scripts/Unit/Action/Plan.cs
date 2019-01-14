using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plan {
	private List<Command> commandOrder;
	private List<Action> actionOrder;

	private int nextCommand;
	private int nextAction;

	private bool isInterrupted;

	public Plan() {
		commandOrder = new List<Command>();
		actionOrder = new List<Action>();

		nextCommand = 0;
		nextAction = 0;
	} 

	public int GetApCost() {
		int total = 0;
		foreach(Action action in actionOrder) {
			total += action.descriptor.cost;
		}
		return total;
	}

	public void QueueAction(Action action, Direction dir, Timeline timeline) {
		actionOrder.Add(action);
		foreach(Frame frame in action.frames) {
			Command c = new Command();
				c.frame = frame;
				c.dir = dir;
				c.type = action.descriptor.actionType;
				commandOrder.Add(c);
		}
	}

	public void RemoveLastAction() {
		if(actionOrder.Count > 0) {
			Action lastAction = actionOrder[actionOrder.Count - 1];	
			actionOrder.RemoveAt(actionOrder.Count - 1);
			for(int i = 0; i < lastAction.frames.Count; i++) {
				commandOrder.RemoveAt(commandOrder.Count - 1);
			}
		}
	}

	public KeyValuePair<Action, Command> PeekNext() {
		KeyValuePair<Action,Command> next = default(KeyValuePair<Action, Command>);
		if(commandOrder.Count > 0 && actionOrder.Count > 0) {
			if(isInterrupted) {
				next = new KeyValuePair<Action, Command>(actionOrder[nextAction], GetInterruptedCommand(commandOrder[nextCommand]));	 
			} else {
				next = new KeyValuePair<Action, Command>(actionOrder[nextAction], commandOrder[nextCommand]);	
			}
		}
		return next;
	}

	public KeyValuePair<Action, Command> ExecuteNext() {
		KeyValuePair<Action,Command> next = default(KeyValuePair<Action, Command>);

		if(nextCommand < commandOrder.Count && nextAction < actionOrder.Count) {
			if(isInterrupted) {
				next = new KeyValuePair<Action, Command>(actionOrder[nextAction], GetInterruptedCommand(commandOrder[nextCommand]));
			}else {
				next = new KeyValuePair<Action, Command>(actionOrder[nextAction], commandOrder[nextCommand]);
			}
			IncrementNextExecutable();
		}
		return next;
	}

	public void InterruptNext() {
		this.isInterrupted = true;
	}

	public void ResetInterrupt() {
		this.isInterrupted = false;	
	}

	public Command GetInterruptedCommand(Command command) {
		Command interruptedCommand = new Command();

		interruptedCommand.frame = actionOrder[nextAction].defaultFrame;
		interruptedCommand.dir = command.GetRelativeDir();
		interruptedCommand.type = command.type; 
		return interruptedCommand;
	}

	//Getters
	public Command GetCommand(int index) {
		if(index >= 0 && index < commandOrder.Count) {
			return commandOrder[index];
		}
		return null;
	}

	public Action GetAction(int index) {
		if(index >= 0 && index < actionOrder.Count) {
			return actionOrder[index];
		}
		return null;
	}

	public int GetTotalFramesCount() {
		return commandOrder.Count;
	}

	public int GetTotalActionsCount() {
		return actionOrder.Count;
	}

	public int GetRemainingFramesCount() {
		return commandOrder.Count - nextCommand;
	}

	public int GetRemainingActionCount() {
		return actionOrder.Count - nextAction;
	}

	public void Flush() {
		 ResetInterrupt();
		 commandOrder.Clear();
		 actionOrder.Clear();
		 nextCommand = 0;
		 nextAction = 0;
	}

	private void IncrementNextExecutable() {
		int newActionIndex;
		nextCommand++;
		newActionIndex = ComputeActionIndex(nextCommand);
		
		if(newActionIndex > nextAction) {
			ResetInterrupt();
		}

		nextAction = newActionIndex;
	}

	private int ComputeActionIndex(int commandIndex) {
		int actionIndex = 0;
		foreach(Action action in actionOrder) {
			commandIndex -= action.frames.Count;
			if(commandIndex < 0) {
				break;
			}
			actionIndex++;
		}
		return actionIndex;
	}
}