using UnityEngine;

public class NormalTowerAttack : TowerAttack
{
    [SerializeField] BulletScript Bullet;

    protected override void Attack(GameObject sender)
    {

        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {
            if (Physics.Raycast(FirePoint.position, FirePoint.forward,
                out RaycastHit hitInfo, range, Tower.Instance.EntityLayer))
            {
                SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
                Entity enemy = hitInfo.collider.GetComponent<Entity>();
                if (enemy != null)
                {


                    if (damageType == DamageType.Normal)
                    {
                        BulletScript bullet = Instantiate(Bullet, FirePoint.position
                            , Quaternion.identity);
                        bullet.SetData(towerData,
                            enemy.transform.position - FirePoint.position);
                    }

                    if (enemy.Health <= 0)
                    {
                        TowerEvents.TowerKilledEntity(gameObject);
                    }
                }

            }

            currentCoolDown = coolDown;
        }
    }
}
