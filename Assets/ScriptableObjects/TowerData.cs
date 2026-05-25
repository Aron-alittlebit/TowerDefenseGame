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
    //public int Tier { get; private set; }

    //public TowerData()
    //{
    //    Tier = 1;
    //}

    //public void Upgrade()
    //{
    //    if (Tier >= 5) return;
    //    Tier++;
    //    Damage += 2 * Tier;
    //    Range += 2 * Tier;
    //    CoolDown -= 0.1f * Tier;
    //}
}
