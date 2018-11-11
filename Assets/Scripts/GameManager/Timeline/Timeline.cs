using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline : MonoBehaviour {
	public int maxFrame;

	//TODO: Also given board
	void Play(List<Unit> units ) {
		for(int i = 0 ; i < maxFrame; i++) {
			//Execute All Unit Commands
			for(int j = 0; j < units.Count; j ++) {
				//Give Next Frame to Timeline
				if(units[j].plan.Count > 0) {
					Command command = units[j].plan.Dequeue();
					command.frame.Execute(units[j], command.dir);
				}
			}

			//Check For Dead Units
			//TODO: Delay
		}
	}
}