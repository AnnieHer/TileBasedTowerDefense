using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager
{
    private PlayerValue playerValue_health;
    private PlayerValue playerValue_money;
    private MapLogic _mapLogic;
    public void SetReferenceToMap(MapLogic mapLogic) {
        _mapLogic = mapLogic;
        playerValue_health = new PlayerValue("Health", 144, 144, 1000, 0, true, true, true);
        playerValue_money = new PlayerValue("Money", 200, 200, 100000, 0, true, false, false);
    }
    public bool ChangeValueHealth(int value) {
        playerValue_health.ChangeValue(value);
        GlobalGet.instance.getPlayerControls().GetComponentByID(0).GetComponent<Slider>().value = playerValue_health.GetValue();
        GlobalGet.instance.getPlayerControls().GetComponentByID(1).GetComponent<TMP_Text>().text = $"{playerValue_health.GetValue()}";

        return playerValue_health.GetValue() <= 0;
    }
    public bool ChangeValueMoney(int value) {
        return playerValue_money.ChangeValue(value);
    }
}
