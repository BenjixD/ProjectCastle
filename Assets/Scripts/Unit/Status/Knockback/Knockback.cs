using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Action {
	void Start() {
		this.frames = new List<Frame>();
        Frame frame = new KnockbackFrame();
        this.frames.Add(frame);
	}
}
