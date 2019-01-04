using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePoke: Action {
	void Start() {
		//Add frames in order
		this.frames = new List<Frame>();
        this.frames.Add(new LancePokeFrameAttack());
        this.frames.Add(new LancePokeFrameAttack());
        this.frames.Add(new LancePokeFrameEnd());
        this.frames.Add(new LancePokeFrameEnd());
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