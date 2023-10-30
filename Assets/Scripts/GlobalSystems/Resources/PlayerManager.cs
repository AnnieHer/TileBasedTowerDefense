using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
