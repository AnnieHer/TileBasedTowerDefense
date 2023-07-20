using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;
    private Tile _selectedTile;
    private void Awake() {
        instance = this;
    }
    public void SendTile(Tile tile) {
        _selectedTile = tile;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _selectedTile.selected) {
            CameraControls.Instance.Center(_selectedTile.transform.position);
        }
    }
}
