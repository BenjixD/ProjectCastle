using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveDescriptor : ActionDescriptor {
	
	public override Action GetNewActionInstance() {
		return new Move(this);
	}

	public override IEnumerator Select(Unit unit, Board board, Timeline timeline, IEnumerator next)
    {
        ActionUI ui = Instantiate(actionUI.gameObject, gameObject.transform).GetComponent<ActionUI>();
        ui.Initialize(unit, GetNewActionInstance(), board, timeline);
        while(ui.state != ActionSubmissionState.SUBMITTED && ui.state != ActionSubmissionState.CANCELLED) {
            yield return new WaitForFixedUpdate();
        }
        Destroy(ui.gameObject);
        StartCoroutine(next);
        yield return null;
    }
}