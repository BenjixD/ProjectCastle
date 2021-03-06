using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    EMPTY = 0,
    PLAINS = 1
}

public class Tile : MonoBehaviour {

    public string tileName;
    public TileType tileType;
    public Unit unit { get; set; }
    //TODO: private Building building;
    public Vector2 coordinate;

    public void PlaceUnit(Unit unit)
    {
        this.unit = unit;
        unit.tile = this;
        foreach(SpriteRenderer sprite in unit.gameObject.GetComponentsInChildren<SpriteRenderer>()) {
            sprite.sortingOrder = GetSortingOrder();
        }
    }

    public int GetSortingOrder() {
        return (int)(coordinate.x * 1000 + coordinate.y * 10); //TODO: there has to be a better formula
    }

    public void RemoveUnit()
    {
        this.unit = null;
    }
}
