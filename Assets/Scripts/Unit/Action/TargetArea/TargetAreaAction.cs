using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetAreaAction : Action {

	public int rangeMin;
	public int rangeMax;
	public Dictionary<HitboxType, int> damage;

	public Tile target { get; set; }

	public TargetAreaAction(ActionDescriptor descriptor) : base(descriptor) {

	}
}
