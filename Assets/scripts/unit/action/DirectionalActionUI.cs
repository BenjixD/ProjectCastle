using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalActionUI : ActionUI {

	//TODO: create a new class for the preview
	public GameObject actionPreview;
    Piece previewPiece;
	Direction dir = Direction.NONE;

	void Update () {
		if(state == ActionSubmissionState.ACTIVE) {
			//TODO: start with no direction?
			if (Input.GetKeyDown("up"))
			{
                ChangePreviewDirection(Direction.UP);
			}
			else if (Input.GetKeyDown("down"))
			{
				dir = Direction.DOWN;
			}
			else if (Input.GetKeyDown("left"))
			{
				dir = Direction.LEFT;
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
        if (previewPiece == null)
        {
            previewPiece = Instantiate(actionPreview, this.transform.position, Quaternion.identity).GetComponent<Piece>();
        }
        int timesToRotate = (newDir - dir) % 4;
        for (int i = timesToRotate; i > 0; i--)
        {
            previewPiece.RotateClockwise();
        }
        dir = newDir;
    }

	public override void SubmitInput() {
		unit.QueueAction(action, dir, timeline);
		state = ActionSubmissionState.SUBMITTED;
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
