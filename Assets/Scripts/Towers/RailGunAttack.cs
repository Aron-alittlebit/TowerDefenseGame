using UnityEditor.PackageManager;
using UnityEngine;

public class RailGunAttack : TowerAttack
{
    protected override void Attack(GameObject sender)
    {

        if (sender != gameObject) return;
        if (currentCoolDown <= 0)
        {

            RaycastHit[] colliders = Physics.SphereCastAll(FirePoint.position, 2f, FirePoint.forward
                , range, Tower.Instance.EntityLayer);
            SoundManager.instance.PlaySound(towerData.ShootingSound, transform, 30f);
            if (colliders.Length > 0)
            {

                foreach(var col in colliders)
                {
                    Entity enemy = col.collider.GetComponent<Entity>();
                    if (enemy != null)
                    {
                        
                        enemy.TakeDamage(damage);
                        if (enemy.Health <= 0)
                        {
                            TowerEvents.TowerKilledEntity(gameObject);
                        }
                    }
                }

                

            }

            currentCoolDown = coolDown;
        }
    }
}
