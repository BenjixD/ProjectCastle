using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public GameObject cursor;
    public GameObject deployPhaseControl;
    public MoveInput moveInput;

    public void BeginDeploymentPhase()
    {
        cursor.GetComponent<Cursor>().movementEnabled = true;
        deployPhaseControl.SetActive(true);
    }
}
