using UnityEngine;

public class LightningTowerAttack : TowerAttack
{
    //protected override void OnEnable()
    //{
    //    GunEvents.OnTowerAttack += Attack;
    //}

    //protected override void OnDisable()
    //{
    //    GunEvents.OnTowerAttack -= Attack;
    //}

    //protected override void Update()
    //{
    //    currentCoolDown -= Time.deltaTime;
    //    Debug.Log(currentCoolDown);
    //}
    protected override void Attack(GameObject sender)
    {
        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, Range,
            Tower.Instance.EntityLayer);
            foreach (var collider in colliders)
            {

                Entity enemy = collider.GetComponent<Entity>();
                enemy.TakeDamage(Damage);

            }
            currentCoolDown = CoolDown;
        }
    }
}
