using System;
using UnityEngine;

public static class TowerEvents
{
   
    public static event Action<int> OnGemSpent;

    public static void GemSpent(int gems) => OnGemSpent?.Invoke(gems);
}
