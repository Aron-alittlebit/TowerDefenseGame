using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    int damage = 100;
    int range = 100;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    void Update()
    {
        if (animator == null) Debug.Log("hellnah");
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Hit");
            GunEvents.GunShoot(firePoint, damage, range);
        }
    }
}
