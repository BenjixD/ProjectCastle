using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : Action {

	public HashSet<Unit> victims;

	public SwordSlash(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.frames = new List<Frame>();
		this.frames.Add(new SwordSlashFrameAttack(this));
		this.frames.Add(new SwordSlashFrameAttack(this));
		this.frames.Add(new SwordSlashFrameAttack(this));
		this.frames.Add(new SwordSlashFrameEnd(this));
		this.frames.Add(new SwordSlashFrameEnd(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();
	}
}