using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Action {
	public Vector2 vector;
	
	public Knockback(Vector2 vector, ActionDescriptor descriptor) : base(descriptor) {
		this.vector = vector;
		this.AddFrame(new KnockbackFrameEffect(this), new KnockbackFrameAnim(this));
	}
}