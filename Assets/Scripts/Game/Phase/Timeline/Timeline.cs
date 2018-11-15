using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline : Phase {
	public int maxFrame;
	public float frameDelay;

    public UIManager uiManager;

    public override IEnumerator Play(Game game, IEnumerator next) {
		Debug.Log("Playing Timeline!");
		for(int i = 0 ; i < maxFrame; i++) {
            uiManager.UpdateTimelineDisplay(i);
            PlayFrame(game.players, game.board);
			yield return new WaitForSeconds(frameDelay);
		}

		StartCoroutine(next);
		yield return null;
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