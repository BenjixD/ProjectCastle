using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Action {
	public Knockback(ActionDescriptor descriptor) : base(descriptor) {
		this.frames = new List<Frame>();
		Frame frame = new KnockbackFrame(this);
		this.frames.Add(frame);
	}
}