using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public Timeline timeline;
	//public List<Unit> units;
	public List<Player> players;
	public Board board;

	void Start() {
		board.InitializeBoard();
		for(int i = 0; i < players.Count; i++) {
			players[i].InitializePlayer(i, CoordsForPlayerSetup(i), board);
		}
	}

	public void PlayTimeline(){
		StartCoroutine(timeline.Play(players, board));
	}

	public Vector2 CoordsForPlayerSetup(int player) {
        if(player % players.Count == 0) {
        	return new Vector2(0,0);
        } else if(player % players.Count == 1) {
        	return new Vector2(board.rows - 1, board.cols - 1);
        } else {
        	return new Vector2(-1, -1);
        }
    }
}