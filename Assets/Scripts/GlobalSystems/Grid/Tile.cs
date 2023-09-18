using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tileType;
    private bool occupied;
    [SerializeField] private Material mainColour, offsetColour, pathColour, startColour, endColour;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private GameObject highlighter;

    public void Init(bool isOffset) {
        occupied = false;
        _renderer.material = isOffset ? offsetColour : mainColour;
    }
    public int GetTileType() {
        return tileType;
    }
    public bool CheckOccupied() {
        return occupied;
    }
    public void Select() {
        highlighter.SetActive(true);
    }
    public void Deselect() {
        highlighter.SetActive(false);
    }
    public void ChangeToTower() {
        occupied = true;
    }
    public void ChangeToPath() {
        occupied = true;
        _renderer.material = pathColour;
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    public void ChangeToStart() {
        occupied = true;
        _renderer.material = startColour;
    }
    public void ChangeToEnd() {
        occupied = true;
        _renderer.material = endColour;
    }
}
