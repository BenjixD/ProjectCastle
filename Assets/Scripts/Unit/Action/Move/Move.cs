using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
	void Start() {
		//Add Move frames in order
		this.frames = new List<Frame>();
        Frame frame = new MoveFrame();
        frame.icon = icons[0];
        this.frames.Add(frame);
	}

    public override IEnumerator Select(Unit unit, Timeline timeline, IEnumerator next)
    {
        ActionUI ui = Instantiate(actionUI.gameObject, gameObject.transform).GetComponent<ActionUI>();
        ui.Initialize(unit, this, timeline);
        while(ui.state != ActionSubmissionState.SUBMITTED && ui.state != ActionSubmissionState.CANCELLED) {
            yield return new WaitForFixedUpdate();
        }
        Destroy(ui.gameObject);
        StartCoroutine(next);
        yield return null;
    }
}