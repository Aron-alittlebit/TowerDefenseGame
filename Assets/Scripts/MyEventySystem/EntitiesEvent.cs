using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action<GameObject> OnEntityReadyToAttack;
    public static event Action<int> OnEntityDeath;
    public static event Action OnStartSpawning;
    public static event Action<bool> OnWeaponSet;
    public static event Action<int> OnEntityRevived;
    public static event Action<bool, GameObject> OnChargeStateChanged;
    
    



    public static void EntityAttack(GameObject sender) 
        => OnEntityReadyToAttack?.Invoke(sender);
    public static void EntityDeath(int id) => OnEntityDeath?.Invoke(id);
    public static void StartSpawning() => OnStartSpawning?.Invoke();
    public static void SetWeapon(bool value) => OnWeaponSet?.Invoke(value);
    public static void ReviveEntity(int id) => OnEntityRevived?.Invoke(id);
    public static void ChargeStateChanged(bool IsInCharge, GameObject sender) =>
        OnChargeStateChanged?.Invoke(IsInCharge, sender);

}
