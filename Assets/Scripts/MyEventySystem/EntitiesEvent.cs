using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action<LivingAbstractClass, GameObject> OnEntityReadyToAttack;
    public static event Action<int> OnEntityDeath;
    public static event Action OnStartSpawning;
    public static event Action<bool> OnWeaponSet;
    
    

    public static void EntityAttack(LivingAbstractClass entity, GameObject sender) 
        => OnEntityReadyToAttack?.Invoke(entity, sender);
    public static void EntityDeath(int id) => OnEntityDeath?.Invoke(id);
    public static void StartSpawning() => OnStartSpawning?.Invoke();
    public static void SetWeapon(bool value) => OnWeaponSet?.Invoke(value);
    
   
}
