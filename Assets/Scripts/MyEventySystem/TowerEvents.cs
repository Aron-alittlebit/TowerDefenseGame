using System;
using UnityEngine;

public static class TowerEvents
{
   
    public static event Action<int> OnGemSpent;
    public static event Action<int> OnTowerSold;
    public static event Action<TowerData> OnTowerBuilt;
    public static event Action<int> OnTowerUpgraded;

    public static void GemSpent(int gems) => OnGemSpent?.Invoke(gems);
    public static void TowerSold(int gems) => OnTowerSold?.Invoke(gems);
    public static void TowerBuilt(TowerData td) => OnTowerBuilt?.Invoke(td);
    public static void TowerUpgraded(int tier) => OnTowerUpgraded?.Invoke(tier);
}
