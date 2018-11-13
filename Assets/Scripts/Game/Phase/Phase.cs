using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Phase : MonoBehaviour {
	public abstract IEnumerator Play(List<Player> players, Board board, IEnumerator next);
}