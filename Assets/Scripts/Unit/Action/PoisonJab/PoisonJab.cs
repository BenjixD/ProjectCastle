using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonJab : Action {

	public HashSet<Unit> victims;

	public PoisonJab(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.AddFrame(new PoisonJabFrameEffectAttack(this), new PoisonJabFrameAnimAttack(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();
	}
}