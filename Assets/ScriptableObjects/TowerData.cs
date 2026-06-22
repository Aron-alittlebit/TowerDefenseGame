using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    public int Cost;
    public string Name;
    public Sprite Icon;
    public Tower TowerPrefab;
    public List<GameObject> TierPrefabs;
    public string TowerName;
    public int Damage;
    public int Range;
    public float CoolDown;
    
}
