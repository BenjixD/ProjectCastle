using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestUnitAction : MonoBehaviour {
	public Unit unit;
  public Timeline timeline;

	void Start() {

	}

	void FixedUpdate() {
		if (Input.GetKeyDown("up")) {
            unit.QueueAction(unit.skills[0], Direction.UP, timeline);
    }
    else if (Input.GetKeyDown("down")) {
     	unit.QueueAction(unit.skills[0], Direction.DOWN, timeline);   
    }
    else if(Input.GetKeyDown("left")) {
    	unit.QueueAction(unit.skills[0], Direction.LEFT, timeline);
    }
		else if(Input.GetKeyDown("right")) {
			unit.QueueAction(unit.skills[0], Direction.RIGHT, timeline);
		}
	}
}