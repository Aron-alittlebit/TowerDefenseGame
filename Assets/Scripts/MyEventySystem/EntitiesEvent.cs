using System;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action OnEntityReadyToAttack;
    public static event Action<string> OnEntityDeath;

    public static void EntityAttack() => OnEntityReadyToAttack?.Invoke();
    public static void EntityDeath(string name) => OnEntityDeath?.Invoke(name);
}
