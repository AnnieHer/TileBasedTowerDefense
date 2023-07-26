using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        if (Input.GetKeyDown(KeyCode.Mouse0) && _selectedTile.selected) {
            CameraControls.Instance.Center(_selectedTile.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 coords = GridManager.Instance.GetRandomPointInGrid();
            Vector3 centerCoords = new Vector3(coords.x, 0f, coords.y);
            CameraControls.Instance.Center(centerCoords);
        }
    }
}
