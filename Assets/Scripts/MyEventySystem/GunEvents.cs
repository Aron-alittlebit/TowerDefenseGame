using System;
using UnityEngine;

public static class GunEvents
{
    public static event Action<Transform, int, int> OnGunShoot;
    public static event Action<TowerData, GameObject> OnTowerAttack;
   

    public static void GunShoot(Transform pos, int dam, int range) 
        => OnGunShoot?.Invoke(pos,dam,range);  
    public static void TowerAttack(TowerData tower, GameObject sender) 
        => OnTowerAttack?.Invoke(tower, sender);
}
