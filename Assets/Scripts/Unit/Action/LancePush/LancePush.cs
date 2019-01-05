using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePush : Action {
	void Start() {
		//Add frames in order
		this.frames = new List<Frame>();
		this.frames.Add(new LancePushFrameAttack());
		this.frames.Add(new LancePushFrameAttack());
		this.frames.Add(new LancePushFrameEnd());
		this.frames.Add(new LancePushFrameEnd());
	}

	public override IEnumerator Select(Unit unit, Board board, Timeline timeline, IEnumerator next)
	{
		ActionUI ui = Instantiate(actionUI.gameObject, gameObject.transform).GetComponent<ActionUI>();
		ui.Initialize(unit, this, board, timeline);
		while (ui.state != ActionSubmissionState.SUBMITTED && ui.state != ActionSubmissionState.CANCELLED)
		{
			yield return new WaitForFixedUpdate();
		}
		Destroy(ui.gameObject);
		StartCoroutine(next);
		yield return null;
	}
}