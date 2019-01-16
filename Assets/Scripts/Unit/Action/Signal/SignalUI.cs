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
            if (board.CheckCoord(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.UP))))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.UP)));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.RIGHT))))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.RIGHT)));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.DOWN))))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.DOWN)));
            }
            else if (board.CheckCoord(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.LEFT))))
            {
                target = board.GetTile(unit.tile.coordinate + (i * Action.GetDirectionVector(Direction.LEFT)));
            }
            // break if set
            if (target)
                break;
        }
        Debug.Log("Signal start, player pos " + unit.tile.coordinate.ToString() + " targetting " + target.coordinate.ToString());
        //Cancel action if target cannot be set
    }

    void Update()
    {
        if (state == ActionSubmissionState.ACTIVE)
        {
            if (Input.GetKeyDown("up"))
            {
                if (board.CheckCoord(target.coordinate + Action.GetDirectionVector(Direction.UP)) && IsInRange(target.coordinate + Action.GetDirectionVector(Direction.UP)))
                {
                    target = board.GetTile(target.coordinate + Action.GetDirectionVector(Direction.UP));
                    Debug.Log("Targetting " + target.coordinate.ToString());
                }
            }
            else if (Input.GetKeyDown("down"))
            {
                if (board.CheckCoord(target.coordinate + Action.GetDirectionVector(Direction.DOWN)) && IsInRange(target.coordinate + Action.GetDirectionVector(Direction.DOWN)))
                {
                    target = board.GetTile(target.coordinate + Action.GetDirectionVector(Direction.DOWN));
                    Debug.Log("Targetting " + target.coordinate.ToString());
                }
            }
            else if (Input.GetKeyDown("left"))
            {
                if (board.CheckCoord(target.coordinate + Action.GetDirectionVector(Direction.LEFT)) && IsInRange(target.coordinate + Action.GetDirectionVector(Direction.LEFT)))
                {
                    target = board.GetTile(target.coordinate + Action.GetDirectionVector(Direction.LEFT));
                    Debug.Log("Targetting " + target.coordinate.ToString());
                }
            }
            else if (Input.GetKeyDown("right"))
            {
                if (board.CheckCoord(target.coordinate + Action.GetDirectionVector(Direction.RIGHT)) && IsInRange(target.coordinate + Action.GetDirectionVector(Direction.RIGHT)))
                {
                    target = board.GetTile(target.coordinate + Action.GetDirectionVector(Direction.RIGHT));
                    Debug.Log("Targetting " + target.coordinate.ToString());
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
            if (Mathf.Abs(unit.tile.coordinate.x - target.coordinate.x) - Mathf.Abs(unit.tile.coordinate.y - target.coordinate.y) >= 0)
            {
                if (target.coordinate.x >= unit.tile.coordinate.x)
                    approxDir = Direction.DOWN;
                else
                    approxDir = Direction.UP;
            }
            else
            {
                if (target.coordinate.y >= unit.tile.coordinate.y)
                    approxDir = Direction.RIGHT;
                else
                    approxDir = Direction.LEFT;
            }
            Debug.Log("Sending signal flare, facing " + approxDir.ToString());
            unit.QueueAction(s, approxDir, timeline);
            state = ActionSubmissionState.SUBMITTED;
        }
    }

    private bool IsInRange(Vector2 coord)
    {
        float taxiDistance = Mathf.Abs(unit.tile.coordinate.x - coord.x) + Mathf.Abs(unit.tile.coordinate.y - coord.y);
        Debug.Log(" taxidistance = " + taxiDistance);
        if (taxiDistance > Signal.RANGEMAX|| taxiDistance < Signal.RANGEMIN)
        {
            Debug.Log("Going out of range!");
            return false;
        }
        return true;
    }
}
