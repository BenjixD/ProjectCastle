using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public int playerId;
	public string playerName;

	public int gold;

	public GameObject kingObject;   //Note unit[0] holds a reference to king

	public List<Unit> units { get; private set; }

	public Cursor cursor;

    //TODO:List of Cards

	public void InitializePlayer(int playerId, Vector2 coord, Board board)
	{
		this.playerId = playerId;
		//this.board = board;
		units = new List<Unit>();

		GameObject kingObj = Instantiate(kingObject, board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, gameObject.transform);
		Unit king = kingObj.GetComponent<Unit>();

        //set unit values
        board.GetTile((int)coord.x, (int)coord.y).PlaceUnit(king);
        king.owner = this;

        units.Add(king);

        //update cursor
        cursor.SetCoords(coord);
    }

    public void AddUnit(Unit unit) {
        unit.owner = this;
        units.Add(unit);
    }

	public void RemoveUnit(Unit unit)
	{
		units.Remove(unit);
	}

	public bool IsOwner(Unit unit) {
		var found = units.Find(x => x == unit);
		return found != null;
	}
}