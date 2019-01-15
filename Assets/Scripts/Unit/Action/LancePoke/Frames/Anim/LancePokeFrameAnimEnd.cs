using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePokeFrameAnimEnd : FrameAnim {
	
	public LancePokeFrameAnimEnd(Action instance) : base(instance) {}

	public override bool ExecuteAnimation(SimulatedDisplacement sim, Direction dir, Board board) {
        //Clean Up all spawned anims
        foreach(KeyValuePair<Tile, GameObject> pair in ((LancePoke)action).spawnedLanceAnims) {
        	UnitAnimator animator = pair.Value.GetComponent<UnitAnimator>();
        	animator.SetBool("EndAnimation", true);
        }

        //Remove references
        ((LancePoke)action).spawnedLanceAnims.Clear();
        return true;
	}
}