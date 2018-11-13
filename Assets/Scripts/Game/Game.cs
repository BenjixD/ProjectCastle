using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {
	//public List<Unit> units;
	public List<Player> players;
	public List<Phase> phases;
	public Board board;

	private int turn = 0;
	public bool nextTurn = true;

	void Start() {
		//Init Board
		board.InitializeBoard();

		//Init Players
		for(int i = 0; i < players.Count; i++) {
			players[i].InitializePlayer(i, CoordsForPlayerSetup(i), board);
		}
	}

	void FixedUpdate() {
		if(nextTurn) {
			//Init Turn, Insert Phases in reverse order
			IEnumerator next = EndTurn();
			for(int i = phases.Count - 1; i >= 0; i--) {
				next = phases[i].Play(players, board, next);
			}
			IEnumerator start = StartTurn(next);

			//Start the Game
			nextTurn = false;
			StartCoroutine(start);
		}
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

    private IEnumerator StartTurn(IEnumerator next) {
    	//TODO: Start Turn
    	turn++;
    	Debug.Log("Turn: " + turn);
    	StartCoroutine(next);
    	yield return null;
    }

    private IEnumerator EndTurn() {
    	//TODO: End Turn
    	Debug.Log("End Turn");
    	nextTurn = true;
    	yield return null;
    }
}