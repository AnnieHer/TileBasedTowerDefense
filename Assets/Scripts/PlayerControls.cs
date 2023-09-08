using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;
    [SerializeField] private TowerSO selectedTower;
    [SerializeField] private LayerMask gridTileLayer;
    private Tile _selectedTile;
    private Camera _camera;
    Ray pointerRay;
    private void Awake() {
        instance = this;
        _camera = Camera.main;
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
        
        pointerRay = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(pointerRay, out RaycastHit hitinfo, 1000, gridTileLayer)) {
            if (_selectedTile != hitinfo.transform.GetComponent<Tile>() && _selectedTile != null)
                _selectedTile.Deselect();
            _selectedTile = hitinfo.transform.GetComponent<Tile>();
            _selectedTile.Select();
        }
        else if (_selectedTile != null) {
            _selectedTile.Deselect();
            _selectedTile = null;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && _selectedTile != null) {
            
            CameraControls.Instance.Center(_selectedTile.transform.position);
            if (!_selectedTile.CheckOccupied()) {
                TowerManager.instance.SpawnTower(_selectedTile.transform.position, selectedTower);
                _selectedTile.ChangeToTower();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 coords = GridManager.Instance.GetRandomPointInGrid();
            Vector3 centerCoords = new Vector3(coords.x, 0f, coords.y);
            CameraControls.Instance.Center(centerCoords);
        }
    }
}
