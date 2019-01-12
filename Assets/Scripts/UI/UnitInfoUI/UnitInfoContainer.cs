using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfoContainer : MonoBehaviour {

	UnitInfoUI curUnitInfoUI;

	public void UpdateUnitInfoDisplay(Player viewer, Unit unit)
	{
		if (unit != null) {
			if (curUnitInfoUI == null) {
				curUnitInfoUI = Instantiate(unit.unitInfoUI, transform).GetComponent<UnitInfoUI>();
			}
			else if (curUnitInfoUI.GetType() == unit.unitInfoUI.GetType()) {
				curUnitInfoUI.gameObject.SetActive(true);
			}
			else {
				Destroy(curUnitInfoUI.gameObject);
				curUnitInfoUI = Instantiate(unit.unitInfoUI, transform).GetComponent<UnitInfoUI>();
			}
			curUnitInfoUI.UpdateInformation(viewer, unit);
		} else if (curUnitInfoUI != null) {
			curUnitInfoUI.gameObject.SetActive(false);
		}
	}
}