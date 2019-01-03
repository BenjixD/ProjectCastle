using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalActionUI : ActionUI {

	public GameObject actionPreview;
    ActionPiece actionPiece;
    public Vector2 actionOrigin;

    //TODO: remove after testing
    Unit testUnit;

    void Start()
    {
        //TODO: for testing; remove after first Action is created and tested
        state = ActionSubmissionState.ACTIVE;
        unit = testUnit;
    }

    void Update () {
		if(state == ActionSubmissionState.ACTIVE) {
			if (Input.GetKeyDown("up"))
			{
                ChangePreviewDirection(Direction.UP);
            }
            else if (Input.GetKeyDown("down"))
			{
                ChangePreviewDirection(Direction.DOWN);
			}
			else if (Input.GetKeyDown("left"))
            {
                ChangePreviewDirection(Direction.LEFT);
            }
            else if (Input.GetKeyDown("right"))
			{
                ChangePreviewDirection(Direction.RIGHT);
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

	void ChangePreviewDirection(Direction newDir)
	{
        if (actionPiece == null)
        {
            actionPiece = Instantiate(actionPreview).GetComponent<ActionPiece>();
            actionPiece.absoluteOrigin = unit.tile.coordinate;
        }
        actionPiece.ChangeDirection(newDir);
    }

    public override void SubmitInput() {
        if (actionPiece != null)
        {
            unit.QueueAction(action, actionPiece.dir, timeline);
            state = ActionSubmissionState.SUBMITTED;
        }
	}

	public override void CancelInput()
	{
		state = ActionSubmissionState.CANCELLED;
	}

	public override bool CanAddAction(Action action)
	{
		if (!unit.CanConsumeAp(action.cost + GetApCost()))
		{
			Debug.Log("Unit cannnot consume anymore AP!");
			return false;
		}
		else if (!unit.CanUseFrame(action.frames.Count + GetFrameCost(), timeline))
		{
			Debug.Log("Unit cannot exceed total number of frames!");
			return false;
		}
		return true;
	}
}
