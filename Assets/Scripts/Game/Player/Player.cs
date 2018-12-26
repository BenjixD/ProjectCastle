using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public InputManager inputManager;

	public int playerId;

	public int gold;

	public GameObject kingObject;   //Note unit[0] holds a reference to king
	public List<Unit> units { get; private set; }

	//private Board board;

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
        king.skills[0].inputManager = inputManager;

        units.Add(king);
    }

    public void AddUnit(Unit unit) {
        unit.owner = this;
        units.Add(unit);
    }

	public void RemoveUnit(Unit unit)
	{
		units.Remove(unit);
	}
}