
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    
    public Transform FirePoint;
    protected float currentCoolDown;
    

    protected virtual void Start()
    {

        currentCoolDown = 0;
        
    }
    protected virtual void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
    }

    protected virtual void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
    }

    
    protected virtual void Update()
    {
        currentCoolDown -= Time.deltaTime;
        Debug.Log(currentCoolDown);
    }

    protected virtual void Attack(TowerData towerData, GameObject sender)
    {
        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out RaycastHit hitInfo,
                towerData.Range, Tower.Instance.EntityLayer))
            {
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                
                enemy.TakeDamage(towerData.Damage);
            }
            
            currentCoolDown = towerData.CoolDown;
        }
    }

    


}
