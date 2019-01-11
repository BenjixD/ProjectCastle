using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaitDescriptor : ActionDescriptor {

	public override Action GetNewActionInstance() {
		return new Wait(this);
	}

	public override IEnumerator Select(Unit unit, Board board, Timeline timeline, IEnumerator next) {
		//TODO: Create a Wait UI
        StartCoroutine(next);
        yield return null;
    }
}