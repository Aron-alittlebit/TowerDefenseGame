
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public TowerData towerData;
    public Transform FirePoint;
    protected float CoolDown;
    protected float currentCoolDown;
    protected int Damage;
    

    protected void Start()
    {
        Damage = towerData.Damage;
        CoolDown = towerData.CoolDown;
        currentCoolDown = CoolDown;
        
    }
    protected void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
    }

    protected void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
    }

    
    protected void Update()
    {
        currentCoolDown -= Time.deltaTime;
    }

    protected void Attack(float Range)
    {

        if (currentCoolDown <= 0)
        {
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out RaycastHit hitInfo,
                Range, Tower.Instance.EntityLayer))
            {
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                
                enemy.TakeDamage(Damage);
            }
            currentCoolDown = CoolDown;
        }
    }

    


}
