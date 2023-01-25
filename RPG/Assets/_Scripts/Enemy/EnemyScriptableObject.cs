using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;

    public float health;

    public float speed;
    public int damage;
}
