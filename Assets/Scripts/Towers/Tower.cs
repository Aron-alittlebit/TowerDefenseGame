using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Tower : LivingAbstractClass
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    public int Cost { get; private set; }
    public int Tier { get; private set; }
    //public string Name { get; private set; }
    public bool IsBuilt { get; private set; }
   
    void Awake()
    {
        IsBuilt = false;
        Tier = 1;
        Instance = this;
        if(GetComponentInChildren<TowerRotator>() != null)
            GetComponentInChildren<TowerRotator>().enabled = false;
        if (GetComponentInChildren<TowerAttack>() != null)
            GetComponentInChildren<TowerAttack>().enabled = false;
    }

   

    public void TowerIsBuilt()
    {
        if(GetComponentInChildren<TowerRotator>() != null)
            GetComponentInChildren<TowerRotator>().enabled = true;
        if (GetComponentInChildren<TowerAttack>() != null)
            GetComponentInChildren<TowerAttack>().enabled = true;
        IsBuilt = true;
    }

   

    public void SetCost(int cost)
    {
        Cost = cost;
    }

    public void IncreaseTier()
    {
        if(Tier>=4) return;
        Tier += 1;
    }

    public TowerData towerData;
    GameObject Visual;

    protected override void Start()
    {
        base.Start();
        Visual = transform.GetChild(0).gameObject;
    }

    public void UpgradeVisual()
    {
        DestroyImmediate(Visual);
        Visual = Instantiate(
            towerData.TierPrefabs[Tier - 2],
            transform.position,
            transform.rotation,
            transform
        );

        

        //TowerEvents.TowerBuilt(towerData, gameObject);
    }


}
