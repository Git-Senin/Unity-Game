using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGrid : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] TileBase tileBase1;
    [SerializeField] TileBase tileBase2;
    private void Awake()
    {
        int i = Random.Range(0, 2);

        Debug.Log(i);
        if (i == 1)
            tileMap.SetTile(Vector3Int.zero, tileBase1);
        else
            tileMap.SetTile(Vector3Int.zero, tileBase2);
    }
}
