using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnSetup : Phase {
	public override IEnumerator Play(List<Player> players, Board board, IEnumerator next) {
		Debug.Log("Started TurnSetup Phase");
		ResetAllAp(players);

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