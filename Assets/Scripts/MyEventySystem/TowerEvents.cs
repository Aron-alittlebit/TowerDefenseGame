using System;
using UnityEngine;

public static class TowerEvents
{
   
    public static event Action<int> OnGemSpent;
    public static event Action<int> OnTowerSold;
    public static event Action<TowerData, GameObject> OnTowerBuilt;
    
    public static event Action<Tower, GameObject> OnTowerUpgraded;
    public static event Action<GameObject> OnTowerKilledEntity;
    

    public static void GemSpent(int gems) => OnGemSpent?.Invoke(gems);
    public static void TowerSold(int gems) => OnTowerSold?.Invoke(gems);
    public static void TowerBuilt(TowerData td, GameObject sender) => OnTowerBuilt?.Invoke(td, sender);
    public static void TowerUpgraded(Tower t, GameObject sender) 
        => OnTowerUpgraded?.Invoke(t, sender);
    public static void TowerKilledEntity(GameObject sender) => OnTowerKilledEntity?.Invoke(sender);
}
