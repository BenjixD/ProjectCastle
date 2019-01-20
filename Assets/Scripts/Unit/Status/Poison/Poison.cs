using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Action {
	public Poison(ActionDescriptor descriptor) : base(descriptor) {
		this.AddFrame(new PoisonFrameEffect(this), new PoisonFrameAnim(this));
	}
}