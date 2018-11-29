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

	public GameObject piece;

	public bool movementEnabled = false;
	public GameObject testPiece; //TODO: remove after finished testing

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
				if (board.CheckCoord(currCoords + new Vector2(-1, 0)))
				{
					currCoords = new Vector2(currCoords.x - 1, currCoords.y);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("down"))
			{
				if (board.CheckCoord(currCoords + new Vector2(1, 0)))
				{
					currCoords = new Vector2(currCoords.x + 1, currCoords.y);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("left"))
			{
				if (board.CheckCoord(currCoords + new Vector2(0, -1)))
				{
					currCoords = new Vector2(currCoords.x, currCoords.y - 1);
					OnCursorAction();
				}
			}
			if (Input.GetKeyDown("right"))
			{
				if (board.CheckCoord(currCoords + new Vector2(0, 1)))
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
			if (Input.GetKeyDown(KeyCode.B) && piece == null)
			{
				Debug.Log("Testing piece spawned.");
				BeginPiecePlacement(testPiece);
			}
			// Piece placement inputs
			if (piece != null)
			{
				if (Input.GetKeyUp("a"))
				{
					piece.GetComponent<Piece>().RotateCounterClockwise();
				}
				else if (Input.GetKeyUp("d"))
				{
					piece.GetComponent<Piece>().RotateClockwise();
				}
				else if (Input.GetKeyDown("return"))
				{
					//TODO: if necessary, make + open menu before placing piece (confirmation menu?)
					Debug.Log("Piece placed.");
					board.PlacePieceAsPossible(piece.GetComponent<Piece>(), currCoords);
					EndPiecePlacement();
				}
				else if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
				{
					EndPiecePlacement();
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
			// True modulo, not C#'s
			index = index - playerUnits.Count * Mathf.Floor(index / playerUnits.Count);
			SetCoord(playerUnits[(int)index].tile.coordinate);
		}
		else
		{
			//TODO: move to controlling player's king or something
		}
	}

	public void BeginPiecePlacement(GameObject piece)
	{
		this.piece = Instantiate(piece, this.transform.position, Quaternion.identity);
		piece.GetComponent<Piece>().board = board;
		deploymentMenusControl.SetActive(false);
	}

	public void EndPiecePlacement()
	{
		Destroy(piece);
		piece = null;
		deploymentMenusControl.SetActive(true);
	}

	public void OnCursorAction()
	{
		UpdateCursorLocation();
		UpdatePieceLocation();
	}

	void UpdateCursorLocation()
	{
		gameObject.transform.position = board.CoordToPosition(currCoords);
		cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
		selectedUnit = board.GetTile(currCoords).unit;
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
			piece.transform.position = board.CoordToPosition(currCoords);
		}
	}
}
