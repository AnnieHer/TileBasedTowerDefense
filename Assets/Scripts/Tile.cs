using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private int tileType;
    private bool occupied;
    public bool selected;
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
    private void OnMouseEnter() {
        highlighter.SetActive(true);
        PlayerControls.instance.SendTile(this);
        selected = true;
    }
    private void OnMouseExit() {
        highlighter.SetActive(false);
        selected = false;
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
