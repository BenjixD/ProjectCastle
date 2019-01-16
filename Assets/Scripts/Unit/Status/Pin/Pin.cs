using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : Action {
	public Vector2 vector;

	public Pin(Vector2 vector, ActionDescriptor descriptor) : base(descriptor) {
		this.vector = vector;
		this.AddFrame(new PinFrameEffect(this), new PinFrameAnim(this));
	}
}
