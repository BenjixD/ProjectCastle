using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Action {
	public int distance;

	public Blink(int distance, ActionDescriptor descriptor) : base(descriptor) {
		this.distance = distance;
		this.AddFrame(new BlinkFrameEffect(this), new BlinkFrameAnim(this));
        this.SetDefaultFrame(new DefaultFrameEffect(this), new DefaultFrameAnim(this));
	}
}
