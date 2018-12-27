using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deployment : Phase {
	public override IEnumerator Play(Game game, IEnumerator next) {
		Debug.Log("Started Deployment Phase!");
        foreach (Player player in game.players) {
            game.uiManager.ShowDeploymentPhaseUI(player.playerName);
            player.cursor.gameObject.SetActive(true);
			while(player.cursor.state != CursorState.END) {
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("Player " + player.playerId + "confirmed deployment!");
            player.cursor.gameObject.SetActive(false);
        }
        StartCoroutine(next);
		yield return null;
	}
}