using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline : MonoBehaviour {
	public int maxFrame;
	public float frameDelay;

	public IEnumerator Play(List<Player> players, Board board) {
		for(int i = 0 ; i < maxFrame; i++) {
			PlayFrame(players, board);
			yield return new WaitForSeconds(frameDelay);
		}
	}

	private void PlayFrame(List<Player> players, Board board) {
		//Move units one frame
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				ExecuteUnitCommand(unit, board);
			}
		}

		//kill all units who satisfy death condition
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				KillUnitWhenDead(unit, board);
			}
		}
	}

	private void ExecuteUnitCommand(Unit unit, Board board) {
		if(unit.plan.Count > 0) {
			Command command = unit.plan.Dequeue();
			command.frame.Execute(unit, command.dir, board);
		}
	}

	private void KillUnitWhenDead(Unit unit, Board board) {
		if(unit.IsDead()) {
			//TODO: Death Anim then death
		}
	}
}