using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] float CoolDown;
    float currentCoolDown;
    [SerializeField] int Damage;
    
    bool IsBuilt;

    private void Start()
    {
        currentCoolDown = CoolDown;
        IsBuilt = false;
    }
    private void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
        TowerEvents.OnTowerBuilt += IsBuiltChanger;
    }

    private void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
        TowerEvents.OnTowerBuilt += IsBuiltChanger;
    }

    void IsBuiltChanger(bool value)
    {
        IsBuilt = value;
    }
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
        
    }

    void Attack(Transform firePoint, Vector3 targetPos, float Range)
    {
        //if (IsBuilt) return;
        if (currentCoolDown <= 0)
        {
            
            Vector3 direction =targetPos - firePoint.position;
            
            if (Physics.Raycast(firePoint.position, direction, out RaycastHit hitInfo,
                Range, Tower.Instance.EntityLayer))
            {
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                enemy.TakeDamage(Damage);
            }
            currentCoolDown = CoolDown;
        }
    }


}
