using System;
using UnityEngine;

public static class GunEvents
{
    public static event Action<Transform, int, int> OnGunShoot;

    public static void GunShoot(Transform pos, int dam, int range) => OnGunShoot?.Invoke(pos,dam,range);  
}
