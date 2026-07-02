using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Animator animator;
    bool IsEnabled;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        IsEnabled = false;
    }
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Hit");
            GunEvents.GunShoot();
        }
    }

    public void SetBlade()
    {
        GunEvents.SetBlade(!IsEnabled);
    }

    
}
