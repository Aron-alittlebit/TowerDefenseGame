using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Scriptable Objects/TowerData")]
public class TowerData : ScriptableObject
{
    public int Cost;
    public Tower TowerPrefab;
    public string TowerName;
    public int Damage;
    public float Range;
    public float CoolDown;
}
