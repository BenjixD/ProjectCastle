using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
    public ControlManager control;

    void Start() {
		//Add Move frames in order
		this.frames = new List<Frame>();
        Frame frame = new MoveFrame();
        frame.icon = icons[0];
        this.frames.Add(frame);
	}

    public override void Select(Unit unit)
    {
        control.directionalInput.BeginInput(unit, this);
    }
}