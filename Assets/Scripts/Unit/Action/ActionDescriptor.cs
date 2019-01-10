using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class ActionDescriptor : MonoBehaviour {
	public string actionName;
	public int cost;
	public ActionType actionType;
	public ActionUI actionUI;
	public GameObject[] icons;

	public abstract Action GetNewActionInstance();

	public virtual IEnumerator Select(Unit unit, Board board, Timeline timeline, IEnumerator next) {
		StartCoroutine(next);
		yield return null;
	}
}