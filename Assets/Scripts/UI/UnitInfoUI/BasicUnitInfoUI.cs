using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnitInfoUI : UnitInfoUI {

	public override void UpdateInformation(Player viewer, Unit unit)
	{
		if (unit != null)
		{
			information.SetActive(true);
			unitName.text = unit.unitName;
			healthBarContent.fillAmount = (float)unit.curHp / unit.maxHp;
			healthText.text = unit.curHp.ToString() + "/" + unit.maxHp.ToString();
			if (viewer == unit.owner)
			{
				apText.text = (unit.maxAp - unit.GetConsumedAp()) + "/" + unit.maxAp + " AP";
			} else {
				apText.text = unit.maxAp.ToString() + " AP";
			}
			foreach (KeyValuePair<string, StatusEffect> statusEffect in unit.statusController) {
				//TODO: list status effect names/icons and durations
			}
		} else
		{
			information.SetActive(false);
		}
	}
}
