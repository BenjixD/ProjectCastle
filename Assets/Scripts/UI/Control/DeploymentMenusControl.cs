﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeploymentMenusControl : MonoBehaviour
{
    public Board board;
    public MenuManager menuManager;
    public Cursor cursor;

    void Update()
    {
		if (Input.GetKeyDown("return"))
		{
			cursor.movementEnabled = false;
			Tile tile = board.GetTile(0,0);
			//Tile tile = board.GetTile((int)cursor.currCoords.x, (int)cursor.currCoords.y);
			Unit unit = tile.unit;
			if (unit != null)
			{
				//TODO: if unit belongs to current player:
				menuManager.OpenActionsMenu(unit, cursor);
			}
			else
			{
				menuManager.OpenPhaseMenu();
			}
		}
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
        {
			menuManager.CloseAllMenus();
			cursor.movementEnabled = true;
        }
	}
}
