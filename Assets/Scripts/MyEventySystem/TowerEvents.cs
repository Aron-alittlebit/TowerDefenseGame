using System;
using UnityEngine;

public static class TowerEvents
{
    public static event Action<bool> OnTowerBuilt;

    public static void TowerBuilt(bool IsBuilt) => OnTowerBuilt?.Invoke(IsBuilt);   
}
