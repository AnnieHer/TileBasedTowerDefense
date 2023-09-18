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
    }
}
