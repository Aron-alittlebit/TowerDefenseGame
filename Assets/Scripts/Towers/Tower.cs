using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Tower : LivingAbstractClass
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    public TowerData towerData { get; private set; }
    GameObject Visual;

    public Sprite Icon {  get; private set; }
    public int Cost { get; private set; }
    public int Tier { get; private set; }
    public string Name { get; private set; }
    public bool IsBuilt { get; private set; }

    public int KillCount { get; private set; }
   
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

    

    public void SetTowerData(TowerData td)
    {
        towerData = td;
        Cost = towerData.Cost;
        Name = towerData.name;
        Icon = towerData.Icon;
    }

    public void IncreaseTier()
    {
        if(Tier>=4) return;
        Tier += 1;
    }
    protected virtual void OnEnable()
    {
        TowerEvents.OnTowerKilledEntity += IncreaseKillCount;
    }

    protected virtual void OnDisable()
    {
        TowerEvents.OnTowerKilledEntity -= IncreaseKillCount;
    }
    


    protected override void Start()
    {
        base.Start();
        Visual = transform.GetChild(0).gameObject;
    }

    public void UpgradeVisual()
    {   
        IncreaseTier();
        DestroyImmediate(Visual);
        
        Visual = Instantiate(
            towerData.TierPrefabs[Tier - 2],
            transform.position,
            transform.rotation,
            transform
        );

    }

    void IncreaseKillCount(GameObject sender)
    {
        if (transform.GetChild(0).gameObject != sender) return;

        KillCount++;
    }

    public void SetHealth(int value)
    {
        StartingHealth = value;
        health = StartingHealth;
        
    }


}
