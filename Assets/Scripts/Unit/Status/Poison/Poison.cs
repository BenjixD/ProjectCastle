using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Action {
	public Poison(ActionDescriptor descriptor) : base(descriptor) {
		this.frames = new List<Frame>();
		Frame frame = new PoisonFrame(this);
		this.frames.Add(frame);
	}
}