using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action<LivingAbstractClass> OnEntityReadyToAttack;
    public static event Action<int> OnEntityDeath;
    public static event Action OnStartSpawning;
    public static event Action<List<Vector3>> OnSetPath;
    

    public static void EntityAttack(LivingAbstractClass entity) 
        => OnEntityReadyToAttack?.Invoke(entity);
    public static void EntityDeath(int id) => OnEntityDeath?.Invoke(id);
    public static void StartSpawning() => OnStartSpawning?.Invoke();
    public static void SetPath(List<Vector3> path) => OnSetPath?.Invoke(path);
   
}
