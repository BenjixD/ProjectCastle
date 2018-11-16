using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    public Board board;
    private Camera cam;
    public UIManager uiManager;

    public Vector2 startCoords;
    public Vector2 currCoords { get; set; }

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
                    UpdateAfterMovement();
                }
            }
            if (Input.GetKeyDown("down"))
            {
                if (board.CheckCoord((int)currCoords.x + 1, (int)currCoords.y))
                {
                    currCoords = new Vector2(currCoords.x + 1, currCoords.y);
                    UpdateAfterMovement();
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (board.CheckCoord((int)currCoords.x, (int)currCoords.y - 1))
                {
                    currCoords = new Vector2(currCoords.x, currCoords.y - 1);
                    UpdateAfterMovement();
                }
            }
            if (Input.GetKeyDown("right"))
            {
                if (board.CheckCoord((int)currCoords.x, (int)currCoords.y + 1))
                {
                    currCoords = new Vector2(currCoords.x, currCoords.y + 1);
                    UpdateAfterMovement();
                }
            }
        }
    }

    public void UpdateAfterMovement()
    {
        gameObject.transform.position = board.CoordToPosition((int)currCoords.x, (int)currCoords.y);
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
        Unit currUnit = board.GetTile((int)currCoords.x, (int)currCoords.y).unit;
        if (currUnit != null && currUnit.plan.Count > 0)
        {
            uiManager.DisplayTimelineIcons(currUnit.plan);
        }
        else
        {
            uiManager.DisplayTimelineIcons(null);
        }
    }
}
