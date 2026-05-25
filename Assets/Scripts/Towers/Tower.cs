using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    public int Cost { get; private set; }
   
    void Awake()
    {
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

    
}
