using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSetup : Phase {
	public override IEnumerator Play(Game game, IEnumerator next) {
		Debug.Log("Started TurnSetup Phase");
		ResetAllAp(game.players);

		StartCoroutine(next);
		yield return null;
	}

	private void ResetAllAp(List<Player> players) {
		foreach(Player player in players) {
			foreach(Unit unit in player.units) {
				unit.RefreshAp();
			}
		}
	}
}