using UnityEngine;


public class GunDamage : MonoBehaviour
{
    
    [SerializeField] int Damage = 100;
    [SerializeField] int Range = 100;

    
    private void OnEnable()
    {
        GunEvents.OnGunShoot += Shoot;
    }

    private void OnDisable()
    {
        GunEvents.OnGunShoot -= Shoot;
    }
    public void Shoot(Camera cam)
    {
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, Range))
        {
            
            Entity enemy = hitInfo.collider.GetComponent<Entity>();
            if (enemy != null && enemy.Health > 0)
            {
                enemy.TakeDamage(Damage);
            }
        }
    }

    
}
