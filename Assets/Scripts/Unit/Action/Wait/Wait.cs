using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wait : Action {
    
    void Start() {
		//Add Move frames in order
        this.frames = new List<Frame>();
		this.frames.Add(new WaitFrame());
	}

    public override void Select(Unit unit)
    {
        
    }
}