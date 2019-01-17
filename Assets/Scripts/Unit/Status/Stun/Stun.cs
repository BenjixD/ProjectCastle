using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Action {
	public Stun(ActionDescriptor descriptor) : base(descriptor) {
		this.AddFrame(new StunFrameEffect(this), new StunFrameAnim(this));
	}
}