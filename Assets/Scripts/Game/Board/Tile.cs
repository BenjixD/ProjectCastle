using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public string tileName;
    public Unit unit { get; set; }
    //TODO: private Building building;
    public Vector2 coordinate;

    public void PlaceUnit(Unit unit)
    {
        this.unit = unit;
        unit.tile = this;
    }

    public void RemoveUnit(Unit unit)
    {
        //TODO: if multiple units are allowed on a tile, remove only the given unit
        this.unit = null;
    }
}
