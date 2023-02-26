using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static LevelGrid;

public class LevelGrid : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        Ground,
        Water
    }
    [SerializeField] private Tilemap tileMap;

    [SerializeField] private int length;
    [SerializeField] private int width;

    [Header("Tiles")]
    [SerializeField] private TileBase defaultTile;
    [SerializeField] private TileBase centerTile;
    [SerializeField] private TileBase nodeTile;

    [Header("Noise Map")]
    [SerializeField] private float noiseValue;
    [SerializeField] private float multiplier = 45;
    [SerializeField] private float threshhold = 0.4f;
    [SerializeField] private float uv;

    [Space(20)]
    [SerializeField] private float seed;
    [SerializeField] private GameObject bananaPrefab;
    [SerializeField] private GameObject enemyPrefab;

    private List<List<TileType>> map; 
    private void Awake()
    {
        this.map = InitializeMap();
        Player.instance.transform.position = GetCenterFloat();
    }
    private void Start()
    {
        FillMap(TileType.Ground);
        GenerateSphere(150);
        ApplyPerlinNoise();
        GenerateSphere(50);
        this.map = GetWalkableSpaceMap(GetCenterInt());
        FillWater();
        DrawMap();
        SpawnBananas();
        SpawnEnemies();
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(Vector3.zero, Vector3.right * length, Color.white, 1000f);
        Debug.DrawLine(Vector3.zero, Vector3.up *   width, Color.white, 1000f);
    }
    private TileBase GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Ground:
                return defaultTile;
            case TileType.Water:
                return nodeTile;
        }
        return null;
    }
    private void DrawMap()
    {
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), GetTile(map[x][y]));
            }
        }
    }
    private List<List<TileType>> InitializeMap()
    {
        // Map 
        List<List<TileType>> map = new List<List<TileType>>();
        // X
        for (int x = 0; x < length; x++)
        {
            map.Add(new List<TileType>());

            // Y
            for (int y = 0; y < width; y++)
            {
                map[x].Add(TileType.Empty);
            }
        }

        return map;
    }
    private void FillMap(TileType tileType = TileType.Empty)
    {
        for (int x = 0; x < length; x++)
        {

            for (int y = 0; y < width; y++)
            {
                map[x][y] = tileType;
            }
        }
    }
    private void ApplyPerlinNoise()
    {
        seed = Random.Range(0, 1_000);
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                noiseValue = Mathf.PerlinNoise(
                    (x / multiplier) + seed,
                    (y / multiplier));

                if (noiseValue > threshhold)
                {
                    if (map[x][y] == TileType.Ground)
                        map[x][y] = TileType.Empty;
                }
            }
        }
    }
    private void GenerateTerrain()
    {
        seed = Random.Range(0, 1_000);
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                noiseValue = Mathf.PerlinNoise(
                    (x / multiplier) + seed,
                    (y / multiplier));

                if (noiseValue > threshhold)
                {
                    map[x][y] = TileType.Ground;
                }
            }
        }
    }
    private void GenerateSphere(int radius)
    {
        Vector2 center = GetCenterFloat();
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                if (distance <= radius)
                {
                    map[x][y] = TileType.Ground;
                }
            }
        }
    }
    private void DrawBorder()
    {
        for(int x = 0; x < length; x++)
        {
            tileMap.SetTile(new Vector3Int(x, -1, 0), centerTile);
            tileMap.SetTile(new Vector3Int(x, length, 0), centerTile);
        }

        for(int y = 0; y < width; y++)
        {
            tileMap.SetTile(new Vector3Int(-1, y, 0), centerTile);
            tileMap.SetTile(new Vector3Int(width, y, 0), centerTile);
        }
    }
    private List<List<TileType>> GetWalkableSpaceMap(Vector2Int startPoint)
    {
        // BFS Algorithm

        List<List<TileType>> visited = InitializeMap();
        List<List<TileType>> walkable = InitializeMap();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        // Starting point
        queue.Enqueue(startPoint);

        visited[startPoint.x][startPoint.y] = TileType.Ground;
        walkable[startPoint.x][startPoint.y] = TileType.Ground;

        while (queue.Count > 0)
        {
            // Neighbors
            foreach (Vector2Int neighbor in GetNeighbors(queue.Dequeue()))
            {
                // Has Ground and not visited
                if (this.map[neighbor.x][neighbor.y] == TileType.Ground && visited[neighbor.x][neighbor.y] != TileType.Ground)
                {
                    // Explore and add to walkable
                    queue.Enqueue(neighbor);
                    walkable[neighbor.x][neighbor.y] = TileType.Ground;
                }

                // Mark as Visited
                visited[neighbor.x][neighbor.y] = TileType.Ground;
            }
        }

        return walkable; 
    }
    private List<Vector2Int> GetNeighbors(Vector2Int source)
    {
        // Left - Right - Top - Bottom
        List<Vector2Int> neighbors = new List<Vector2Int>();

        // Left
        if (InBounds(source.x - 1, source.y))
            neighbors.Add(new Vector2Int(source.x - 1, source.y));
        // Right
        if (InBounds(source.x + 1, source.y))
            neighbors.Add(new Vector2Int(source.x + 1, source.y));
        // Top
        if (InBounds(source.x, source.y + 1))
            neighbors.Add(new Vector2Int(source.x, source.y + 1));
        // Bottom
        if (InBounds(source.x, source.y - 1))
            neighbors.Add(new Vector2Int(source.x, source.y - 1));

        return neighbors;
    }
    private bool InBounds(int x, int y)
    {
        if (x >= this.length || y >= this.width || x < 0 || y < 0)
            return false;

        return true;
    }
    private Vector2Int GetCenterInt()
    {
        return new Vector2Int(this.length / 2, this.width / 2);
    }
    private Vector2 GetCenterFloat() 
    {
        return new Vector2(this.length / 2, this.width / 2);
    }
    private void FillWater()
    {
        for (int x = 0; x < length; x++)
        {

            for (int y = 0; y < width; y++)
            {
                if (this.map[x][y] != TileType.Ground)
                    this.map[x][y] = TileType.Water;
            }
        }
    }
    private void TeleportToSpawn()
    {
        Player.instance?.transform.SetPositionAndRotation(GetCenterFloat(), Player.instance.transform.rotation);
    }
    
    public void SpawnBananas()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i][j] != TileType.Ground) 
                    continue;

                if (Random.Range(0, 101) > 99)
                {
                    Vector3 mapPos = tileMap.CellToWorld(new Vector3Int(i, j, 0));
                    Instantiate(bananaPrefab, mapPos + new Vector3(0.5f, 0.5f), Quaternion.identity);
                }
            }
        }
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i][j] != TileType.Water)
                    continue;

                if (Random.Range(0, 101) > 99)
                {
                    Vector3 mapPos = tileMap.CellToWorld(new Vector3Int(i, j, 0));
                    Instantiate(enemyPrefab, mapPos + new Vector3(0.5f, 0.5f), Quaternion.identity);
                }
            }
        }
    }
}
