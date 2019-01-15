using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Action {
	public Knockback(ActionDescriptor descriptor) : base(descriptor) {
		this.AddFrame(new KnockbackFrameEffect(this), new KnockbackFrameAnim(this));
	}
}