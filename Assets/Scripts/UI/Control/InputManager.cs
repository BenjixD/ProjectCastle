using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public Cursor cursor;
    public DirectionalInput directionalInput;

    public void InitializeDeploymentPhase()
    {
        cursor.movementEnabled = true;
		cursor.deploymentMenusControl.SetActive(true);
	}
}
