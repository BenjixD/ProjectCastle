using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Custom array class viewable in the inspector
[System.Serializable]
public class CoordArray
{
	public Vector2[] locations;
}

public class UnitPlacer : MonoBehaviour {

	public Board board;
	public Player redPlayer;
	public Player bluePlayer;

	public GameObject[] unitPrefabs;        // Load pairs of units in red, blue, red, blue order
	public bool mirrorUnits;                // Mark as true to copy the red side placements for the blue side
	public CoordArray[] unitLocations;      // Set size as number of unit types, then input how many of each unit you want

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
			board.GetTile((int)coord.x, (int)coord.y).PlaceUnit(unit);
			unit.owner = player;
			player.units.Add(unit);
		}
		else {
			Debug.Log("Unit placement for " + unit.unitName + " at " + coord + " is invalid!");
		}
	}

	void PlaceUnitsForMirrorMatch() {
		for (int i = 0; i < unitPrefabs.Length; i+=2) {
			foreach (Vector2 coord in unitLocations[i].locations) {
				Unit newUnit = Instantiate(unitPrefabs[i], board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, redPlayer.transform).GetComponent<Unit>();
				PlaceUnit(newUnit, coord, redPlayer);
				newUnit = Instantiate(unitPrefabs[i+1], board.CoordToPosition(board.rows - 1 - (int)coord.x, board.cols - 1 - (int)coord.y), Quaternion.identity, bluePlayer.transform).GetComponent<Unit>();
				PlaceUnit(newUnit, new Vector2(board.rows - 1 - (int)coord.x, board.cols - 1 - (int)coord.y), bluePlayer);
			}
		}
	}

	void PlaceUnits() {
		for (int i = 0; i < unitPrefabs.Length; i+=2) {
			foreach (Vector2 coord in unitLocations[i].locations) {
				Unit newUnit = Instantiate(unitPrefabs[i], board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, redPlayer.transform).GetComponent<Unit>();
				PlaceUnit(newUnit, coord, redPlayer);
			}
		}
		for (int i = 1; i < unitPrefabs.Length; i+=2) {
			foreach (Vector2 coord in unitLocations[i].locations) {
				Unit newUnit = Instantiate(unitPrefabs[i], board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, bluePlayer.transform).GetComponent<Unit>();
				PlaceUnit(newUnit, coord, bluePlayer);
			}
		}
	}
}
