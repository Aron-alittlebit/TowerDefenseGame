using System;
using UnityEngine;

public static class GunEvents
{
    public static event Action<Transform, int, int> OnGunShoot;
    public static event Action<float> OnTowerAttack;

    public static void GunShoot(Transform pos, int dam, int range) 
        => OnGunShoot?.Invoke(pos,dam,range);  
    public static void TowerAttack(float Range) 
        => OnTowerAttack?.Invoke(Range);
}
