using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
    public MoveInput moveInput;

    void Start() {
		//Add Move frames in order
		this.frames = new List<Frame>();
		this.frames.Add(new MoveFrame());
	}

    public override void Select(Unit unit)
    {
        Debug.Log("move selected");
        moveInput.BeginInput(unit, this);
    }
}