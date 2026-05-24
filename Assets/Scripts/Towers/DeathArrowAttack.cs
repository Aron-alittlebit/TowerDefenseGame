using UnityEngine;

public class DeathArrowAttack : TowerAttack
{
    protected override void OnEnable()
    {
        GunEvents.OnTowerAttack += Attack;
    }

    protected override void OnDisable()
    {
        GunEvents.OnTowerAttack -= Attack;
    }

    protected override void Update()
    {
        currentCoolDown -= Time.deltaTime;
        
    }
    protected override void Attack(TowerData towerData, GameObject sender)
    {
        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, towerData.Range,
            Tower.Instance.EntityLayer);
            foreach (var collider in colliders)
            {

                Entity enemy = collider.GetComponent<Entity>();
                enemy.TakeDamage(towerData.Damage);

            }
            currentCoolDown = towerData.CoolDown;
        }
    }
}
