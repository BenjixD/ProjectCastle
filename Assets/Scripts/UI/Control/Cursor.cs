using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{

	public Board board;
	private Camera cam;
	public UIManager uiManager;
	public MenuManager menuManager;
	public GameObject deploymentMenusControl;

	public Vector2 startCoords;
	public Vector2 currCoords;

	private Unit selectedUnit;
	private Player selectedUnitOwner;

	public Piece piece;

	public bool movementEnabled = false;
	public bool placingPiece = false;
	public Piece testPiece; //TODO: remove after finished testing

	void Start()
	{
		cam = Camera.main;
		currCoords = startCoords;
	}

	void Update()
	{
		if (movementEnabled)
		{
			if (Input.GetKeyDown("up"))
			{
				if (board.CheckCoord((int)currCoords.x - 1, (int)currCoords.y))
				{
					currCoords = new Vector2(currCoords.x - 1, currCoords.y);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("down"))
			{
				if (board.CheckCoord((int)currCoords.x + 1, (int)currCoords.y))
				{
					currCoords = new Vector2(currCoords.x + 1, currCoords.y);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("left"))
			{
				if (board.CheckCoord((int)currCoords.x, (int)currCoords.y - 1))
				{
					currCoords = new Vector2(currCoords.x, currCoords.y - 1);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("right"))
			{
				if (board.CheckCoord((int)currCoords.x, (int)currCoords.y + 1))
				{
					currCoords = new Vector2(currCoords.x, currCoords.y + 1);
					OnCursorAction();
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
			if (Input.GetKeyDown(KeyCode.B))
			{
				Debug.Log("Piece spawned.");
				placingPiece = true;
				piece = Instantiate(testPiece);
				piece.transform.position = this.transform.position;
				deploymentMenusControl.SetActive(false);
			}
			if (placingPiece)
			{
				if (Input.GetKeyUp("a"))
				{
					piece.RotateCounterClockwise();
				}
				else if (Input.GetKeyUp("d"))
				{
					piece.RotateClockwise();
				}
				else if (Input.GetKeyDown("return"))
				{
					//TODO: if necessary, make + open menu before placing piece (confirmation menu?)
					Debug.Log("Piece placed.");
					board.PlacePiece(piece, currCoords);
					piece = null;
					placingPiece = false;
					deploymentMenusControl.SetActive(true);
				}
			}
		}
	}

	public void SetCoord(Vector2 coords)
	{
		currCoords = coords;
		UpdateCursorLocation();
	}

	void CycleUnits(int skipNum)
	{
		if (selectedUnit != null) // TODO: also check this unit belongs to the player in control of the cursor
		{
			List<Unit> playerUnits = selectedUnitOwner.units;
			float index = playerUnits.FindIndex(unit => unit == selectedUnit);
			index += skipNum;
			// True modulo
			index = index - playerUnits.Count * Mathf.Floor(index / playerUnits.Count);
			SetCoord(playerUnits[(int)index].tile.coordinate);
		}
		else
		{
			//TODO: move to controlling player's king or something
		}
	}

	public void OnCursorAction()
	{
		UpdateCursorLocation();
		UpdatePieceLocation();
	}

	void UpdateCursorLocation()
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

	void UpdatePieceLocation()
	{
		if(piece != null)
		{
			piece.SetPosition(currCoords);
		}
	}
}
