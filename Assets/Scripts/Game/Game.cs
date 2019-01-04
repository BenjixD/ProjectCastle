using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {
	//public List<Unit> units;
	public List<Player> players;
	public List<Phase> phases;
	public Board board;
	public UIManager uiManager;

    private int turn = 0;
	public bool nextTurn = true;

	void Start() {
		//Init Board
		if(board.gameObject.GetComponentsInChildren<Tile>().Length > 0) {
			board.InitializePreBuiltBoard();
		}else {
			board.InitializeBoard();	
		}
		
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
				next = phases[i].Play(this, next);
			}
			IEnumerator start = StartTurn(next);

			//Start the Game
			nextTurn = false;
			StartCoroutine(start);
		}
	}

	public Vector2 CoordsForPlayerSetup(int player) {
        if(player % players.Count == 0) {
        	for(int i = 0; i < board.rows; i++) {
        		for(int j = 0; j < board.cols; j++) {
        			if(board.GetTile(i,j).tileType == TileType.PLAINS && board.GetTile(i,j).unit == null) {
        				return new Vector2(i, j);
        			}
        		}
        	}
        } else if(player % players.Count == 1) {
        	for(int i = board.rows - 1; i >= 0; i--) {
        		for(int j = board.cols - 1; j >= 0; j--) {
        			if(board.GetTile(i,j).tileType == TileType.PLAINS && board.GetTile(i,j).unit == null) {
        				return new Vector2(i, j);
        			}
        		}
        	}
        }

        return new Vector2(-1, -1);
    }

    public Deployment GetDeploymentPhase() {
    	return gameObject.GetComponent<Deployment>();
    }

    public Timeline GetTimelinePhase() {
    	return gameObject.GetComponent<Timeline>();
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