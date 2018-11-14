using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int playerId;

    public int gold;

    public GameObject kingObject;   //Note unit[0] holds a reference to king

    private Board board;
    public List<Unit> units { get; private set; }

    //TODO:List of Cards

    public void InitializePlayer(int playerId, Vector2 coord, Board board)
    {
        this.playerId = playerId;
        this.board = board;
        units = new List<Unit>();

        GameObject kingObj = Instantiate(kingObject, board.CoordToPosition((int)coord.x, (int)coord.y), Quaternion.identity, gameObject.transform);
        Unit king = kingObj.GetComponent<Unit>();

        //set unit values
        board.GetTile((int)coord.x, (int)coord.y).PlaceUnit(king);
        king.owner = this;

        units.Add(king);
    }

    public void addUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void removeUnit(Unit unit)
    {
        units.Remove(unit);
    }
}