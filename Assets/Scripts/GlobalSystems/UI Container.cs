using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] componentsArray;
    public GameObject GetComponentByID(int index) {
        if (index > componentsArray.Length || index < 0)
        return null;
        return componentsArray[index];
    }
    public GameObject GetComponentByName(string name) {
        foreach (GameObject gameObject in componentsArray) {
            if (gameObject.name == name) {
                return gameObject;
            }
        }
        return null;
    }
    public GameObject[] GetArray() {
        return componentsArray;
    }
}

