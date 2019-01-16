using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : Action {

	public HashSet<Unit> victims;

	public SwordSlash(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.AddFrame(new SwordSlashFrameEffectAttack(this), new SwordSlashFrameAnimAttack(this));
		this.AddFrame(new SwordSlashFrameEffectAttack(this), new SwordSlashFrameAnimAttack(this));
		this.AddFrame(new SwordSlashFrameEffectAttack(this), new SwordSlashFrameAnimAttack(this));
		this.AddFrame(new SwordSlashFrameEffectEnd(this), new SwordSlashFrameAnimEnd(this));
		this.AddFrame(new SwordSlashFrameEffectEnd(this), new SwordSlashFrameAnimEnd(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();
	}
}