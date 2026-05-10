using System;
using UnityEngine;

public static class EntitiesEvent
{
    public static event Action OnEntityReadyToAttack;

    public static void EntityAttack() => OnEntityReadyToAttack?.Invoke();
}
