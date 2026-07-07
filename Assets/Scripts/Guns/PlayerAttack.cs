using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Camera MainCamera;
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
            GunEvents.GunShoot(MainCamera);
            SoundManager.instance.PlaySound(
                GetComponent<CharacterChanging>().CurrentHero.ShootingSound,
                transform, 30f);
        }
    }

    public void SetBlade()
    {
        GunEvents.SetBlade(!IsEnabled);
    }

    
}
