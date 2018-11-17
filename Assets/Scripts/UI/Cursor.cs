using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public Board board;
    private Camera cam;
    public UIManager uiManager;

    public Vector2 startCoords;
    public Vector2 currCoords;

    //!!
    public Unit selectedUnit;
    public Player selectedUnitOwner;

    public bool movementEnabled = false;

    void Start()
    {
        cam = Camera.main;
        currCoords = startCoords;
    }

    void Update () {
        if (movementEnabled)
        {
            if (Input.GetKeyDown("up"))
            {
                if (board.CheckCoord((int)currCoords.x - 1, (int)currCoords.y))
                {
                    currCoords = new Vector2(currCoords.x - 1, currCoords.y);
                    UpdateCursorLocation();
                }
            }
            if (Input.GetKeyDown("down"))
            {
                if (board.CheckCoord((int)currCoords.x + 1, (int)currCoords.y))
                {
                    currCoords = new Vector2(currCoords.x + 1, currCoords.y);
                    UpdateCursorLocation();
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (board.CheckCoord((int)currCoords.x, (int)currCoords.y - 1))
                {
                    currCoords = new Vector2(currCoords.x, currCoords.y - 1);
                    UpdateCursorLocation();
                }
            }
            if (Input.GetKeyDown("right"))
            {
                if (board.CheckCoord((int)currCoords.x, (int)currCoords.y + 1))
                {
                    currCoords = new Vector2(currCoords.x, currCoords.y + 1);
                    UpdateCursorLocation();
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleUnits(-1);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleUnits(1);
            }
        }
    }

    public void SetCoord(Vector2 coords)
    {
        startCoords = coords;
        UpdateCursorLocation();
    }

    void CycleUnits(int skipNum)
    {
        if (selectedUnit != null) // TODO: also check this unit belongs to the player in control of the cursor
        {
            List<Unit> playerUnits = selectedUnitOwner.units;
            int index = playerUnits.FindIndex(unit => unit == selectedUnit);
            index = (index + skipNum) % playerUnits.Count;
            SetCoord(playerUnits[index].tile.coordinate);
        }
    }

    public void UpdateCursorLocation()
    {
        gameObject.transform.position = board.CoordToPosition((int)currCoords.x, (int)currCoords.y);
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
        selectedUnit = board.GetTile((int)currCoords.x, (int)currCoords.y).unit;
        if (selectedUnit != null)
        {
            selectedUnitOwner = selectedUnit.owner;
            uiManager.DisplayTimelineIcons(selectedUnit.plan);
        }
        else
        {
            selectedUnitOwner = null;
            uiManager.DisplayTimelineIcons(null);
        }
    }
}
