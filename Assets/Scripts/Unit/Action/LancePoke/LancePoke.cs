using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LancePoke: Action {

    public HashSet<Unit> victims;
    public Dictionary<Tile, GameObject> spawnedLanceAnims;

	public LancePoke(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
        this.AddFrame(new LancePokeFrameEffectAttack(this), new LancePokeFrameAnimAttack(this));
        this.AddFrame(new LancePokeFrameEffectAttack(this), new DefaultFrameAnim(this));
        this.AddFrame(new LancePokeFrameEffectEnd(this), new LancePokeFrameAnimEnd(this));
        this.AddFrame(new LancePokeFrameEffectEnd(this), new DefaultFrameAnim(this));
        //Default Frame
        this.SetDefaultFrame(new LancePokeFrameEffectEnd(this), new LancePokeFrameAnimEnd(this));

        //Initialize "one time hit"
        victims = new HashSet<Unit>();

        //Initialize Spawned Lance Anims
        spawnedLanceAnims = new Dictionary<Tile, GameObject>();
	}

    
}