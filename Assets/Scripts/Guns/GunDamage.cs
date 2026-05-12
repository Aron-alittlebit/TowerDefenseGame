using UnityEngine;


public class GunDamage : MonoBehaviour
{
    private void OnEnable()
    {
        GunEvents.OnGunShoot += Shoot;
    }

    private void OnDisable()
    {
        GunEvents.OnGunShoot -= Shoot;
    }
    public void Shoot(Transform firePoint,int damage, int range )
    {
        
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hitInfo, range))
        {
            
            Entity enemy = hitInfo.collider.GetComponent<Entity>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
