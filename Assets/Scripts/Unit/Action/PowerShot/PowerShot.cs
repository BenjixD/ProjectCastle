using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : Action {

	public PowerShot(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.AddFrame(new PowerShotFrameEffectAttack(this), new PowerShotFrameAnimAttack(this));
		this.AddFrame(new PowerShotFrameEffectEnd(this), new PowerShotFrameAnimEnd(this));
		//Default Frame
		this.SetDefaultFrame(new PowerShotFrameEffectEnd(this), new PowerShotFrameAnimEnd(this));
	}
}