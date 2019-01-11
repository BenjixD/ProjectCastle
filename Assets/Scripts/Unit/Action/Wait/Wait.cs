using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wait : Action {
    public Wait(ActionDescriptor descriptor) : base(descriptor) {
		//Add Move frames in order
        this.frames = new List<Frame>();
		this.frames.Add(new WaitFrame(this));
	}
}