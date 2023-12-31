using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height, amountOfTurns;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private MapLogic gridParent;
    private Dictionary<Vector2, Tile> TileMap;
    private List<Vector2> path;
    public static GridManager Instance;
    private ObjectPool<Tile> _pool;
    [SerializeField] private bool _usePool;
    private void Awake() {
        Instance = this;
    }
    

    private void Start() {
        _pool = new ObjectPool<Tile>(() => {
            return Instantiate(tilePrefab);
        }, Tile => {
            Tile.gameObject.SetActive(true);
        }, Tile => {
            Tile.gameObject.SetActive(false);
        }, Tile => {
            Destroy(Tile.gameObject);
        }, false, 1024, 4096);

        GenerateGrid();
    }
    public void GenerateGrid() {
        
        MapLogic mapLogic = FindObjectOfType<MapLogic>() ? FindObjectOfType<MapLogic>() : Instantiate(gridParent);

        width = int.Parse(GlobalGet.instance.getDebugControls().GetComponentByID(1).GetComponent<TMP_InputField>().text);
        height = int.Parse(GlobalGet.instance.getDebugControls().GetComponentByID(2).GetComponent<TMP_InputField>().text);
        amountOfTurns = int.Parse(GlobalGet.instance.getDebugControls().GetComponentByID(3).GetComponent<TMP_InputField >().text);
        _usePool = GlobalGet.instance.getDebugControls().GetComponentByID(4).GetComponent<Toggle>().isOn;


        TileMap = new Dictionary<Vector2, Tile>();
        Vector2 start = GetRandomPointOnEdge();
        Vector2 end = new Vector2(width - 1 - start.x, height - 1 - start.y);

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                Tile spawnedTile = _usePool ? _pool.Get() : Instantiate(tilePrefab);
                spawnedTile.transform.SetPositionAndRotation(new Vector3(x, Random.Range(-0.2f, 0.2f), z), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {z}";
                spawnedTile.transform.SetParent(mapLogic.transform);
                bool isOffset = (x + z) % 2 == 1;
                spawnedTile.Init(isOffset);

                TileMap[new Vector2(x, z)] = spawnedTile;   
            }
        }
        
        CameraControls.Instance.Center(new Vector3(width/2,0,height/2));
        float distance = (((Mathf.Sqrt(width*width+height*height)) / 2) / Mathf.Tan(Camera.main.fieldOfView * (Mathf.PI/180)) / 2);
        CameraRotation.Instance.SetNewLimit(distance);

        List<Vector2> points = new List<Vector2>(); 
        points.Add(start);
        for (int i = 0; i < amountOfTurns; i++)
            points.Add(GetRandomPointInGrid());
        points.Add(end);
        path = GenerateRandomPath(points);
        ChangeTilesOnPath(path);
        GetTileByPosition(start).ChangeToStart();
        GetTileByPosition(end).ChangeToEnd();   
        mapLogic.SendPath(path.ToArray());
        mapLogic.SetMapSize(new Vector2(width, height));
    }
    public void DestroyGrid() {
        MapLogic mapLogic = FindObjectOfType<MapLogic>();
        int currentX = mapLogic.GetMapSize().x;
        int currentZ = mapLogic.GetMapSize().y;
        for (int x = 0; x < currentX; x++) {
            for (int z = 0; z < currentZ; z++) {
                if (_usePool) {
                _pool.Release(TileMap[new Vector2(x, z)]);
                }
                else {
                Destroy(TileMap[new Vector2(x, z)].gameObject);
                }
            }
        }  
        mapLogic.CleanMap();
    }
    public Tile GetTileByPosition(Vector2 pos) {
        if (TileMap.TryGetValue(pos, out Tile tile)) {
            return tile;
        }
        return null;
    }
    private Vector2 GetRandomPointOnEdge()
    {
        // Generate a random point on one of the edges of the grid
        int side = Random.Range(0, 4); // 0: top, 1: right, 2: bottom, 3: left

        int x = 0, z = 0;
        switch (side)
        {
            case 0: // Top edge
                x = Random.Range(0, width);
                z = height - 1;
                break;
            case 1: // Right edge
                x = width - 1;
                z = Random.Range(0, height);
                break;
            case 2: // Bottom edge
                x = Random.Range(0, width);
                z = 0;
                break;
            case 3: // Left edge
                x = 0;
                z = Random.Range(0, height);
                break; 
        }
        return new Vector2(x, z);
    }
    public Vector2 GetRandomPointInGrid() {
        // Implement the logic to generate a random point within the grid bounds
        // You can use the width and height variables to define the grid bounds
        // For example:
        int x = Random.Range(1, width - 1);
        int z = Random.Range(1, height - 1);
        return new Vector2(x, z);
    }
    private void ChangeTilesOnPath(List<Vector2> path)
    {
        foreach (Vector2 position in path)
        {
            Tile tile = GetTileByPosition(position);
            if (tile != null)
            {
                tile.ChangeToPath();
            }
        }
    }
    private List<Vector2> GenerateRandomPath(List<Vector2> points) {
        List<Vector2> path = new List<Vector2>();

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 currentPoint = points[i];
            Vector2 nextPoint = points[i + 1];

            // Calculate the difference between current and next points
            int dx = (int)nextPoint.x - (int)currentPoint.x;
            int dy = (int)nextPoint.y - (int)currentPoint.y;

            // Move horizontally
            if (dx != 0)
            {
                int stepX = Sign(dx);
                for (int x = (int)currentPoint.x; x != (int)nextPoint.x; x += stepX)
                {
                    path.Add(new Vector2(x, currentPoint.y));
                }
            }

            // Move vertically
            if (dy != 0)
            {
                int stepY = Sign(dy);
                for (int y = (int)currentPoint.y; y != (int)nextPoint.y; y += stepY)
                {
                    path.Add(new Vector2(nextPoint.x, y));
                }
            }
        }

        // Add the last point
        path.Add(points[points.Count - 1]);

        return path;
    }
    public static int Sign(int value)
    {
        if (value < 0)
            return -1;
        else if (value > 0)
            return 1;
        else
            return 0;
    }

    public Vector2[] GetPath()
    {
        return path.ToArray();
    }
}
