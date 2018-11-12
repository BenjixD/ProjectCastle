using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : Action {
	void Start() {
		//Add Move frames in order
		this.frames = new List<Frame>();
		this.frames.Add(new MoveFrame());
	}
}