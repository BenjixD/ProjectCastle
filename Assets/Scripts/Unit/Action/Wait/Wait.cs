using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wait : Action {
    public Wait(ActionDescriptor descriptor) : base(descriptor) {
		//Add Move frames in order
		this.AddFrame(new WaitFrameEffect(this), new WaitFrameAnim(this));

		//Add Default Frame
		this.SetDefaultFrame(new WaitFrameEffect(this), new WaitFrameAnim(this));
	}
}