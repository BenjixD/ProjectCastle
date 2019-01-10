using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
	public Move(ActionDescriptor descriptor) : base(descriptor) {
		//Add Move frames in order
		this.frames = new List<Frame>();
        Frame frame = new MoveFrame(this);
        //frame.icon = icons[0];
        this.frames.Add(frame);
	}
}