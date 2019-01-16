using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : Action {
	public Pin(ActionDescriptor descriptor) : base(descriptor) {
		this.AddFrame(new PinFrameEffect(this), new PinFrameAnim(this));
	}
}
