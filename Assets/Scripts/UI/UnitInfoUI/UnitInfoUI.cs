using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UnitInfoUI : MonoBehaviour {

	public GameObject information;
	public Text unitName;
	public Text healthText;
	public Image healthBarContent;
	public Text apText;

	public abstract void UpdateInformation(Player viewer, Unit unit);
}
