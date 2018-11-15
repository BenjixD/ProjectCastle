using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPiece : MonoBehaviour {
	public Piece piece;

	void FixedUpdate() {
		if(Input.GetKeyUp("a")) {
			piece.RotateCounterClockwise();
		}
		else if(Input.GetKeyUp("d")) {
			piece.RotateClockwise();
		}
	}
}
