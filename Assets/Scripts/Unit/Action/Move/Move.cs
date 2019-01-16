using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
	public Move(ActionDescriptor descriptor) : base(descriptor) {
        //frame.icon = icons[0]
        this.AddFrame(new MoveFrameEffect(this), new MoveFrameAnim(this));

        //Add Default Frame
        this.SetDefaultFrame(new DefaultFrameEffect(this), new DefaultFrameAnim(this));
	}
}