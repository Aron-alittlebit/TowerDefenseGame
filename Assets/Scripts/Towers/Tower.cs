using UnityEngine;

public class Tower : MonoBehaviour
{
    public static Tower Instance;
    public LayerMask EntityLayer;
    
    //public bool IsBuilt;
    
        

    void Awake()
    {
        Instance = this;
        if(GetComponent<TowerRotator>() != null)
            GetComponent<TowerRotator>().enabled = false;
        GetComponent<TowerAttack>().enabled = false;
    }

    public void ChangeIsBuilt(bool value)
    {
        GetComponent<TowerRotator>().enabled = value;
        GetComponent<TowerAttack>().enabled = value;
    }
}
