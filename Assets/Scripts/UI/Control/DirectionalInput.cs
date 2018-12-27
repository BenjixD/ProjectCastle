using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalInput : MonoBehaviour {
	public UIManager uiManager;
	public MenuManager menuManager;
	public Cursor cursor;
	public Timeline timeline;

	private Unit unit;
	private Action skill;
	private bool acceptingInput = false;
	private int actionsCount;

	void Start()
	{
		actionsCount = 0;
	}

	void Update () {
		if (acceptingInput)
		{
			if (Input.GetKeyDown("up"))
			{
				if (unit.QueueAction(skill, Direction.UP, timeline))
				{
					actionsCount++;
				}
			}
			else if (Input.GetKeyDown("down"))
			{
				if (unit.QueueAction(skill, Direction.DOWN, timeline))
				{
					actionsCount++;
				}
			}
			else if (Input.GetKeyDown("left"))
			{
				if (unit.QueueAction(skill, Direction.LEFT, timeline))
				{
					actionsCount++;
				}
			}
			else if (Input.GetKeyDown("right"))
			{
				if (unit.QueueAction(skill, Direction.RIGHT, timeline))
				{
					actionsCount++;
				}
			}
			if (Input.GetKeyDown("return") && actionsCount > 0)
			{
				SubmitInput();
			}
			if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
			{
				if (actionsCount > 0)
				{
					actionsCount = 0;
					unit.FlushPlan();
					//TODO, depending on implementation: refresh unit's curAP, etc.
					Debug.Log("All actions for " + unit.unitName + " cancelled.");
				}
				ReturnToDeployment();
			}
			uiManager.DisplayTimelineIcons(unit.plan);
		}
	}

	public void BeginInput(Unit unit, Action skill)
	{
		this.unit = unit;
		this.skill = skill;
		acceptingInput = true;
	}

	void SubmitInput()
	{
		//TODO: remove if not needed for final implementation
		/*foreach (Direction dir in moves)
		{
			unit.QueueAction(skill, dir, timeline);
		}*/
		ReturnToDeployment();
	}

	void ReturnToDeployment()
	{
		actionsCount = 0;
		acceptingInput = false;
	}
}
