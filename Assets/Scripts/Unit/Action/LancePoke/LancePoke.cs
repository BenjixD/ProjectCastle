using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePoke: Action {

    public HashSet<Unit> victims;

	public LancePoke(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.frames = new List<Frame>();
        this.frames.Add(new LancePokeFrameAttack(this));
        this.frames.Add(new LancePokeFrameAttack(this));
        this.frames.Add(new LancePokeFrameEnd(this));
        this.frames.Add(new LancePokeFrameEnd(this));

        //Initialize "one time hit"
        victims = new HashSet<Unit>();
	}

    
}