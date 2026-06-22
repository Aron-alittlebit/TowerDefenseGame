using Unity.Properties;
using UnityEngine;

public class TowerUIModell
{
    [CreateProperty] public string TowerName { get; set; }
    [CreateProperty] public int Tier { get; set; }
    [CreateProperty] public Sprite TowerIcon { get; set; }

    
    [CreateProperty] public float Damage { get; set; }
    [CreateProperty] public float FireRate { get; set; }
    [CreateProperty] public float Range { get; set; }
    [CreateProperty] public int KillCount { get; set; }

    public TowerUIModell(Tower tower, TowerAttack attack)
    {
        TowerName = tower.Name;
        Tier = tower.Tier;
        TowerIcon = tower.Icon;

        Damage = attack.Damage;
        FireRate = attack.CoolDown;
        Range = attack.Range;
        KillCount = attack.KillCount;
    }
}
