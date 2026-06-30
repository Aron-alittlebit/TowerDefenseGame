using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<HeroData> OnHeroChanged;
    public static event Action OnLevelReloaded;

    public static void HeroChanged(HeroData hd) => OnHeroChanged?.Invoke(hd);
    public static void LevelReloaded() => OnLevelReloaded?.Invoke();
}
