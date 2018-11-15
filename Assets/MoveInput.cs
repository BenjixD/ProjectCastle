using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInput : MonoBehaviour {

    private Unit unit;
    private Action skill;
    private bool acceptingInput = false;
    private List<Direction> moves;
    public GameObject deploymentPhaseControl;

    void Start()
    {
        moves = new List<Direction>();
    }

    void Update () {
        //TODO: check if # of moves surpasses maxFrames
        if (acceptingInput)
        {
            if (Input.GetKeyDown("up"))
            {
                Debug.Log("Move queued: up");
                moves.Add(Direction.UP);
            }
            else if (Input.GetKeyDown("down"))
            {
                Debug.Log("Move queued: down");
                moves.Add(Direction.DOWN);
            }
            else if (Input.GetKeyDown("left"))
            {
                Debug.Log("Move queued: left");
                moves.Add(Direction.LEFT);
            }
            else if (Input.GetKeyDown("right"))
            {
                Debug.Log("Move queued: right");
                moves.Add(Direction.RIGHT);
            }
            if (Input.GetKeyDown("return") && moves.Count > 0)
            {
                SubmitInput();
            }
        }
    }

    public void BeginInput(Unit unit, Action skill)
    {
        this.unit = unit;
        this.skill = skill;
        acceptingInput = true;
    }

    void SubmitInput()
    {
        acceptingInput = false;
        Debug.Log(moves);
        foreach (Direction dir in moves)
        {
            unit.QueueAction(skill, dir);
        }
        //moves.Clear();
        //deploymentPhaseControl.active = true;
        deploymentPhaseControl.SetActive(true);
    }
}
