using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuldingData", menuName = "Scriptable Objects/BuldingData")]
public class BuildingData : ScriptableObject
{
    public int Cost;
    public string Name;
    public Sprite Icon;
    public Tower TowerPrefab;
    public List<GameObject> TierPrefabs;

}
