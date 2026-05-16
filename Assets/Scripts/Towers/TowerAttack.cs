
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] float CoolDown;
    float currentCoolDown;
    [SerializeField] int Damage;
    Transform firePoint;
    
    
    bool IsBuilt;

    private void Start()
    {
        currentCoolDown = CoolDown;
        IsBuilt = false;
    }
    private void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
    }

    private void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
    }

    
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
    }

    void Attack(Transform firePoint, float Range)
    {
        this.firePoint = firePoint;
        
        

        if (currentCoolDown <= 0)
        {
            
            
            

            if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hitInfo,
                Range, Tower.Instance.EntityLayer))
            {
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                
                enemy.TakeDamage(Damage);
            }
            currentCoolDown = CoolDown;
        }
    }

    


}
