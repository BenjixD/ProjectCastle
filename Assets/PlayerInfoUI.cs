using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour {

    public Text playerName;
    public Text goldValue;

    public void UpdateInformation(Player player)
    {
        playerName.text = "Player " + player.playerId;
        goldValue.text = player.gold.ToString();
    }
}
