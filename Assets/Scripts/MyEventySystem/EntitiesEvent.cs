using System;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action OnEntityReadyToAttack;
    public static event Action<int> OnEntityDeath;
    public static event Action OnStartSpawning;
    

    public static void EntityAttack() => OnEntityReadyToAttack?.Invoke();
    public static void EntityDeath(int id) => OnEntityDeath?.Invoke(id);
    public static void StartSpawning() => OnStartSpawning?.Invoke();
   
}
