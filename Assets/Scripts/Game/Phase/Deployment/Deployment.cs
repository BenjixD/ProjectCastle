using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deployment : Phase {
	public override IEnumerator Play(List<Player> players, Board board, IEnumerator next) {
		Debug.Log("Started Deployment Phase!");
		foreach(Player player in players) {
			while(true) {
				if(Input.GetKeyDown("space")) {
					Debug.Log("Player " + player.playerId + "confirmed deployment!");
					break;
				}
				yield return new WaitForFixedUpdate();
			}
		}
		StartCoroutine(next);
		yield return null;
	}

	//Iterate through player turns
	//TODO: 
	private void PlayerTurn(Player player, Board board) {

	}
}