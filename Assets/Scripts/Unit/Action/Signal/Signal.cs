using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Signal : TargetAreaAction {
    /* Test attack for targetted area moves
     * AP: 2
     * Frames: 1 frame startup, 1 frame attack, 1 frame cooldown
     * Range: 2 - 6 tiles away from caster
     * Area of effect: X shape 3 tiles wide
     * Damage: 20 damage at the centre, 10 damage on other tiles
     */

    public HashSet<Unit> victims;

    public Signal(ActionDescriptor descriptor) : base(descriptor) {
		//Add frames in order
        this.AddFrame(new SignalEffectStart(this), new SignalAttackAnim(this));
        this.AddFrame(new SignalEffectAttack(this), new SignalAttackAnim(this));
        this.AddFrame(new SignalEffectEnd(this), new SignalAttackAnim(this));

        //Initialize "one time hit"
        victims = new HashSet<Unit>();

		//Set TargetArea values
		rangeMin = 2;
		rangeMax = 6;
	}
}