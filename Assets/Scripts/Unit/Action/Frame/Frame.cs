using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Frame {
	public FrameEffect effect;
	public FrameAnim anim;

	public Frame(FrameEffect effect, FrameAnim anim) {
		this.effect = effect;
		this.anim = anim;
	}
}