using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorState {
    END = 0,
    ENABLED = 1,
    DISABLED = 2
}

public class Cursor : MonoBehaviour
{
    public Board board;
    public UIManager uiManager;
    public MenuManager menuManager;
    public Player player;

    //TODO: Piece Placement should not be in the Cursor script
    public GameObject piece;

    public GameObject testPiece; //TODO: remove after finished testing

    public CursorState state;

    private Camera cam;
    private Vector2 currCoords;
    private Unit selectedUnit;

    void Awake()
    {
        cam = Camera.main;
    }

    void OnEnable() {
        UpdateCursorLocation();
		UpdateTileInfo();
    }

    void Update()
    {
        if(state == CursorState.ENABLED) {
            if (Input.GetKeyDown("up"))
            {
                MoveCursor(new Vector2(-1, 0));
            }
            if (Input.GetKeyDown("down"))
            {
                MoveCursor(new Vector2(1, 0));
            }
            if (Input.GetKeyDown("left"))
            {
                MoveCursor(new Vector2(0, -1));
            }
            if (Input.GetKeyDown("right"))
            {
                MoveCursor(new Vector2(0, 1));
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleUnits(-1);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleUnits(1);
            }
            if (Input.GetKeyDown("return"))
            {
                Tile tile = board.GetTile(currCoords);
                Unit unit = tile.unit;
                if (unit != null && player.IsOwner(unit))
                {
                    menuManager.OpenActionsMenu(unit, this);
                }
                else
                {
                    //TODO: A different menu for enemy units
                    menuManager.OpenPhaseMenu(this);
                }
            }
            // Piece placement inputs TODO: MOVE TO A DIFFERENT GAMEOBJECT
            if (Input.GetKeyDown(KeyCode.B) && piece == null)
            {
                Debug.Log("Testing piece spawned.");
                BeginPiecePlacement(testPiece);
            }
            else if (piece != null)
            {
                if (Input.GetKeyUp("a"))
                {
                    piece.GetComponent<Piece>().RotateCounterClockwise();
                }
                else if (Input.GetKeyUp("d"))
                {
                    piece.GetComponent<Piece>().RotateClockwise();
                }
                else if (Input.GetKeyDown(KeyCode.B))
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

    public void SetCoords(Vector2 coords)
    {
        currCoords = coords;
    }

    public Vector2 GetCoords() {
        return currCoords;
    }

    public void BeginPiecePlacement(GameObject piece)
    {
        this.piece = Instantiate(piece, this.transform.position, Quaternion.identity);
        piece.GetComponent<Piece>().board = board;
    }

    public void EndPiecePlacement()
    {
        Destroy(piece);
        piece = null;
    }

    public void OnCursorAction()
    {
        UpdateCursorLocation();
        UpdatePieceLocation();
		UpdateTileInfo();
    }

    public void EnableUserInput() {
        state = CursorState.ENABLED;
    }

    public void DisableUserInput() {
        state = CursorState.DISABLED;
    }

    public void EndUserInput() {
        state = CursorState.END;
    }

    void MoveCursor(Vector2 movement)
    {
        if (board.CheckCoord(currCoords + movement))
        {
            currCoords = currCoords + movement;
            OnCursorAction();
        }
    }

    void CycleUnits(int skipNum)
    {
        selectedUnit = board.GetTile(currCoords).unit;
        if (selectedUnit != null && selectedUnit.owner == player)
        {
            float index = player.units.FindIndex(unit => unit == selectedUnit);
            index += skipNum;
            // True modulo, not C#'s
            index = index - player.units.Count * Mathf.Floor(index / player.units.Count);
            SetCoords(player.units[(int)index].tile.coordinate);
            UpdateCursorLocation();
        }
        else
        {
            // Move to this player's first unit
            if (player.units.Count > 0)
            {
                SetCoords(player.units[0].tile.coordinate);
                UpdateCursorLocation();
            }
        }
		OnCursorAction();
    }

    void UpdateCursorLocation()
    {
        gameObject.transform.position = board.CoordToPosition(currCoords);
        cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
    }

    void UpdatePieceLocation()
    {
        if(piece != null)
        {
            piece.transform.position = board.CoordToPosition(currCoords);
        }
    }

	void UpdateTileInfo()
	{
		selectedUnit = board.GetTile(currCoords).unit;
		// Update timeline
		if (selectedUnit != null && player.IsOwner(selectedUnit))
		{
			uiManager.DisplayTimelineIcons(selectedUnit.plan);
		}
		else
		{
			uiManager.DisplayTimelineIcons(null);
		}
		uiManager.DisplayUnitInfo(player, selectedUnit);
	}
}
