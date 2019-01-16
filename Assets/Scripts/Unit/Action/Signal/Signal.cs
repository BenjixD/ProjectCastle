using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Signal: Action {
    /* Test attack for targetted area moves
     * AP: 2
     * Frames: 1 frame startup, 1 frame attack, 1 frame cooldown
     * Range: 2 - 6 tiles away from caster
     * Area of effect: X shape 3 tiles wide
     * Damage: 20 damage at the centre, 10 damage on other tiles
     */

    public const int RANGEMIN = 2;
    public const int RANGEMAX = 6;
    public const int CENTERDMG = 20;
    public const int AOEDMG = 10;

    public HashSet<Unit> victims;

    public Tile Target { get; set; }

    public Signal(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
		this.frames = new List<Frame>();
        this.frames.Add(new SignalStart(this));
        this.frames.Add(new SignalEffect(this));
        this.frames.Add(new SignalEnd(this));

        //Initialize "one time hit"
        victims = new HashSet<Unit>();
	}
}