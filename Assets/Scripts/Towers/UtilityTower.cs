using UnityEngine;

public class UtilityTower : Tower
{
    [SerializeField] float LifeTime;

    protected override void Start()
    {
        Visual = transform.GetChild(0).gameObject;
        Destroy(gameObject, LifeTime);
    }
}
