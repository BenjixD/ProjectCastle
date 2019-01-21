using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerate : TargetAreaAction {

	public HashSet<Unit> victims;

	public Incinerate(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.AddFrame(new IncinerateFrameEffectStart(this), new IncinerateFrameAnimStart(this));
		this.AddFrame(new IncinerateFrameEffectAttack(this), new IncinerateFrameAnimAttack(this));
		this.AddFrame(new IncinerateFrameEffectAttack(this), new IncinerateFrameAnimAttack(this));
		this.AddFrame(new IncinerateFrameEffectAttack(this), new IncinerateFrameAnimAttack(this));
		this.AddFrame(new IncinerateFrameEffectAttack(this), new IncinerateFrameAnimAttack(this));
		this.AddFrame(new IncinerateFrameEffectEnd(this), new IncinerateFrameAnimEnd(this));

		//Initialize "one time hit"
		victims = new HashSet<Unit>();

		//Set TargetArea values
		rangeMin = 2;
		rangeMax = 4;
		damage = new Dictionary<HitboxType, int>();
		damage.Add(HitboxType.OK, 70);
		damage.Add(HitboxType.SOUR, 50);
	}
}