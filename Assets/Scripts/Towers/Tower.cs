using UnityEngine;

public class Tower : LivingAbstractClass
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    public int Cost { get; private set; }
    public int Tier { get; private set; }
   
    void Awake()
    {
        Tier = 1;
        Instance = this;
        if(GetComponent<TowerRotator>() != null)
            GetComponent<TowerRotator>().enabled = false;
        GetComponent<TowerAttack>().enabled = false;
    }

    public void TowerIsBuilt(bool value)
    {
        if(GetComponent<TowerRotator>() != null)
            GetComponent<TowerRotator>().enabled = value;
        GetComponent<TowerAttack>().enabled = value;
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
