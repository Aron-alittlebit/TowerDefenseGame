using System;
using UnityEngine;

public static class GunEvents
{
    public static event Action<Camera> OnGunShoot;
    public static event Action<GameObject> OnTowerAttack;
    public static event Action<bool> OnSetBlade;
   

    public static void GunShoot(Camera cam) => OnGunShoot?.Invoke(cam);  
    public static void TowerAttack(GameObject sender) 
        => OnTowerAttack?.Invoke(sender);

    public static void SetBlade(bool value) => OnSetBlade?.Invoke(value);
}
