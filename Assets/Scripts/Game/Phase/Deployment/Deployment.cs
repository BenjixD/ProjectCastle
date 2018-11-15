using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deployment : Phase {
	public override IEnumerator Play(Game game, IEnumerator next) {
		Debug.Log("Started Deployment Phase!");
        game.control.BeginDeploymentPhase();
		foreach(Player player in game.players) {
			while(!game.endPhase) {
                /*if(Input.GetKeyDown("space")) {
					Debug.Log("Player " + player.playerId + "confirmed deployment!");
					break;
				}*/
                yield return new WaitForFixedUpdate();
            }
            Debug.Log("Player " + player.playerId + "confirmed deployment!");
        }
        game.endPhase = false;
        StartCoroutine(next);
		yield return null;
	}

	//Iterate through player turns
	//TODO: 
	private void PlayerTurn(Player player, Board board) {

	}
}