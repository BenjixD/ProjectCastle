using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePush : Action {

	public HashSet<Unit> victims;

	public LancePush(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.AddFrame(new LancePushFrameEffectAttack(this), new LancePushFrameAnimAttack(this));
		this.AddFrame(new LancePushFrameEffectAttack(this), new LancePushFrameAnimAttack(this));
		this.AddFrame(new LancePushFrameEffectEnd(this), new LancePushFrameAnimEnd(this));
		this.AddFrame(new LancePushFrameEffectEnd(this), new LancePushFrameAnimEnd(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();
	}
}