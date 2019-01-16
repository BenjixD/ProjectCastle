using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalUI : ActionUI
{

    Tile target;
    Signal s;

    void Start()
    {
        s = (Signal)action;
        target = null;
        /* TODO refactor so other skills can use it, also need Direction to Vector2 translation method
        if (Signal.RANGEMIN == 0)
        {
            target = unit.tile;
        }
        */
        
        // Set initial tile to a valid one by lollipopping clockwise
        for (int i = Signal.RANGEMIN; i <= Signal.RANGEMAX; i++)
        {
            if (board.CheckCoord(unit.tile.coordinate + (i * Vector2.up)))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Vector2.up));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Vector2.right)))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Vector2.right));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Vector2.down)))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Vector2.down));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Vector2.left)))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Vector2.left));
            }
            // break if set
            if (target)
                break;
        }

        //Cancel action if target cannot be set
    }

    void Update()
    {
        if (state == ActionSubmissionState.ACTIVE)
        {
            if (Input.GetKeyDown("up"))
            {
                if (board.CheckCoord(target.coordinate + Vector2.up) && IsInRange(target.coordinate + Vector2.up))
                {
                    target = board.GetTile(target.coordinate + Vector2.up);
                }
            }
            else if (Input.GetKeyDown("down"))
            {
                if (board.CheckCoord(target.coordinate + Vector2.down) && IsInRange(target.coordinate + Vector2.down))
                {
                    target = board.GetTile(target.coordinate + Vector2.down);
                }
            }
            else if (Input.GetKeyDown("left"))
            {
                if (board.CheckCoord(target.coordinate + Vector2.left) && IsInRange(target.coordinate + Vector2.left))
                {
                    target = board.GetTile(target.coordinate + Vector2.left);
                }
            }
            else if (Input.GetKeyDown("right"))
            {
                if (board.CheckCoord(target.coordinate + Vector2.right) && IsInRange(target.coordinate + Vector2.right))
                {
                    target = board.GetTile(target.coordinate + Vector2.right);
                }
            }
            else if (Input.GetKeyDown("return"))
            {
                SubmitInput();
            }
            else if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
            {
                CancelInput();
            }
        }
    }

    public override bool CanAddAction(Action action)
    {
        return unit.CanConsumeAp(2) && unit.CanUseFrame(action.frames.Count, timeline);
    }

    public override void CancelInput()
    {
        state = ActionSubmissionState.CANCELLED;
    }

    public override void SubmitInput()
    {
        if (IsInRange(target.coordinate))
        {
            s.Target = target;
            Direction approxDir = Direction.NONE;
            if (Mathf.Abs(target.coordinate.x) - Mathf.Abs(target.coordinate.y) >= 0)
            {
                if (target.coordinate.x >= 0)
                    approxDir = Direction.RIGHT;
                else
                    approxDir = Direction.LEFT;
            }
            else
            {
                if (target.coordinate.y >= 0)
                    approxDir = Direction.UP;
                else
                    approxDir = Direction.DOWN;
            }
            unit.QueueAction(s, approxDir, timeline);
        }
    }

    private Vector2 DirToVector2 (Direction d)
    {
        switch (d)
        {
            case Direction.NONE:
                return Vector2.zero;
            case Direction.UP:
                return Vector2.up;
            case Direction.DOWN:
                return Vector2.down;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    private bool IsInRange(Vector2 coord)
    {
        float taxiDistance = (unit.tile.coordinate - coord).SqrMagnitude();
        if (taxiDistance > Signal.RANGEMAX * Signal.RANGEMAX || taxiDistance < Signal.RANGEMIN * Signal.RANGEMIN)
            return false;
        return true;
    }
}
