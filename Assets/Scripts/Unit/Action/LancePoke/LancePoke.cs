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

    public override IEnumerator Select(Unit unit, Timeline timeline, IEnumerator next)
    {
        unit.plan.QueueAction(this, Direction.RIGHT, timeline);
        StartCoroutine(next);
        yield return null;
    }
}