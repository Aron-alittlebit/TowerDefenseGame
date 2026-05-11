using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    int damage = 100;
    int range = 50;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GunEvents.GunShoot(firePoint, damage, range);
        }
    }
}
