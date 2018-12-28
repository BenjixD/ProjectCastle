using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSetup : Phase {
	public override IEnumerator Play(Game game, IEnumerator next) {
        game.uiManager.ShowTurnSetupPhaseUI();
        Debug.Log("Started TurnSetup Phase");
		ResetAll(game.players);
		StartCoroutine(next);
		yield return null;
	}

	private void ResetAll(List<Player> players) {
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				unit.FlushPlan();
			}
		}
	}
}