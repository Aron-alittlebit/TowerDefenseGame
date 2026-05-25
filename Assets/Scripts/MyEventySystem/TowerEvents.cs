using System;
using UnityEngine;

public static class TowerEvents
{
   
    public static event Action<int> OnGemSpent;
    public static event Action<int> OnTowerSold;

    public static void GemSpent(int gems) => OnGemSpent?.Invoke(gems);
    public static void TowerSold(int gems) => OnTowerSold?.Invoke(gems);
}
