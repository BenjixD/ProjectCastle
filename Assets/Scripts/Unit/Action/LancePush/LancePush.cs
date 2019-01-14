using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePush : Action {

	public HashSet<Unit> victims;

	public LancePush(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.frames = new List<Frame>();
		this.frames.Add(new LancePushFrameAttack(this));
		this.frames.Add(new LancePushFrameAttack(this));
		this.frames.Add(new LancePushFrameEnd(this));
		this.frames.Add(new LancePushFrameEnd(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();
	}
}