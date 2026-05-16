using System;
using UnityEngine;

public static class GunEvents
{
    public static event Action<Transform, int, int> OnGunShoot;
    public static event Action<Transform, Vector3, float> OnTowerAttack;

    public static void GunShoot(Transform pos, int dam, int range) 
        => OnGunShoot?.Invoke(pos,dam,range);  
    public static void TowerAttack(Transform firePoint, Vector3 targetPos, float Range) 
        => OnTowerAttack?.Invoke(firePoint, targetPos, Range);
}
