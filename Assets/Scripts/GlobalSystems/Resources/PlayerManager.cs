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
        _mapLogic.StartCoroutine(AnimationCoroutineHealth(playerValue_health.GetValue()));

        return playerValue_health.GetValue() <= 0;
    }
    public bool ChangeValueMoney(int value) {
        if (playerValue_money.ChangeValue(value)) {
            _mapLogic.StartCoroutine(AnimationCoroutineMoney(playerValue_money.GetValue()));
            return true;
        }
        return false;
    }
    IEnumerator AnimationCoroutineHealth(int value) {
        Slider slider = GlobalGet.instance.getPlayerControls().GetComponentByID(0).GetComponent<Slider>();
        TMP_Text text = GlobalGet.instance.getPlayerControls().GetComponentByID(1).GetComponent<TMP_Text>();
        while (true) {
            yield return null;
            slider.value = Mathf.Lerp(slider.value, value, 0.03f);
            text.text = $"{(int)slider.value}";
            if (Mathf.RoundToInt(slider.value) == value) {
                text.text = $"{value}";
                slider.value = value;
                break;
            }
        }
        if (value == 0) {
            text.text = "Dead";
        }
    }
    IEnumerator AnimationCoroutineMoney(int value) {
        TMP_Text text = GlobalGet.instance.getPlayerControls().GetComponentByID(2).GetComponent<TMP_Text>();
        float lerp = int.Parse(text.text);
        while (true) {
            yield return null;
            lerp = Mathf.Lerp(lerp, value, 0.1f);
            text.text = $"{(int)lerp}";
            
            if (Mathf.RoundToInt(lerp) == value) {
                text.text = $"{value}";
                break;
            }
        }
    }
}
