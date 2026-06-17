using UnityEngine;

public class Tower : LivingAbstractClass
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    public int Cost { get; private set; }
    public int Tier { get; private set; }
    public bool IsBuilt { get; private set; }
   
    void Awake()
    {
        IsBuilt = false;
        Tier = 1;
        Instance = this;
        if(GetComponent<TowerRotator>() != null)
            GetComponent<TowerRotator>().enabled = false;
        GetComponent<TowerAttack>().enabled = false;
    }

    private void Update()
    {
        
    }

    public void TowerIsBuilt()
    {
        if(GetComponent<TowerRotator>() != null)
            GetComponent<TowerRotator>().enabled = true;
        GetComponent<TowerAttack>().enabled = true;
        IsBuilt = true;
    }

    public void SetCost(int cost)
    {
        Cost = cost;
    }

    public void IncreaseTier()
    {
        if(Tier>=5) return;
        Tier++;
    }

    
}
