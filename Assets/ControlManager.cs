using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public GameObject cursor;
    public GameObject deployPhaseControl;
    public DirectionalInput directionalInput;

    public void InitializeDeploymentPhase()
    {
        cursor.GetComponent<Cursor>().movementEnabled = true;
        deployPhaseControl.SetActive(true);
    }
}
