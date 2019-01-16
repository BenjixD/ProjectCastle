using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom array class viewable in the inspector
[System.Serializable]
public class UnitLocations
{
	public GameObject unit;					// Load pairs of units in red, blue, red, blue order
	public Vector2[] locations;
}

public class UnitPlacer : MonoBehaviour {

	public Board board;
	public Player redPlayer;
	public Player bluePlayer;

	public bool mirrorUnits;                // Mark as true to copy the red side placements for the blue side
	public UnitLocations[] unitPlacements;     // Set size as number of unit types, then input how many of each unit you want

	void Start () {
		StartCoroutine(WaitForBoard());
	}

	IEnumerator WaitForBoard()
	{
		yield return new WaitForSeconds(0.1f);
		if (mirrorUnits) {
			PlaceUnitsForMirrorMatch();
		}
		else {
			PlaceUnits();
		}
	}

	void PlaceUnit(Unit unit, Vector2 coord, Player player)
	{
		if(board.CheckCoord(coord) && board.GetTile((int)coord.x, (int)coord.y).tileType != TileType.EMPTY && board.GetTile((int)coord.x, (int)coord.y).unit == null) {
			Unit newUnit = Instantiate(unit, board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, redPlayer.transform).GetComponent<Unit>();
			board.GetTile((int)coord.x, (int)coord.y).PlaceUnit(newUnit);
			newUnit.owner = player;
			player.units.Add(newUnit);
		}
		else {
			Debug.Log("Unit placement for " + unit.unitName + " at " + coord + " is invalid!");
		}
	}

	void PlaceUnitsForMirrorMatch() {
		for (int i = 0; i < unitPlacements.Length; i+=2) {
			foreach (Vector2 coord in unitPlacements[i].locations) {
				PlaceUnit(unitPlacements[i].unit.GetComponent<Unit>(), coord, redPlayer);
				PlaceUnit(unitPlacements[i+1].unit.GetComponent<Unit>(), new Vector2(board.rows - 1, board.cols - 1) - coord, bluePlayer);
			}
		}
	}

	void PlaceUnits() {
		for (int i = 0; i < unitPlacements.Length; i+=2) {
			foreach (Vector2 coord in unitPlacements[i].locations) {
				PlaceUnit(unitPlacements[i].unit.GetComponent<Unit>(), coord, redPlayer);
			}
		}
		for (int i = 1; i < unitPlacements.Length; i+=2) {
			foreach (Vector2 coord in unitPlacements[i].locations) {
				PlaceUnit(unitPlacements[i].unit.GetComponent<Unit>(), coord, bluePlayer);
			}
		}
	}
}
