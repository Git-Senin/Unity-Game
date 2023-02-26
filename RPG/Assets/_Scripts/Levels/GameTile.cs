using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GameTile : ScriptableObject
{
    public enum Owner
    {
        None,
        Ally,
        Enemy,
    }
    public RuleTile tile;
    public GameObject entityOnTile;

    public Owner gameTileOwner;
}