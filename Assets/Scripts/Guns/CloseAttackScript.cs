using UnityEngine;

public class CloseAttackScript : MonoBehaviour
{
    [SerializeField] BoxCollider Blade;
    [SerializeField] int Damage = 100;

    private void Start()
    {
        Blade.enabled = false;
    }

    private void OnEnable()
    {
        GunEvents.OnSetBlade += SetBlade;
    }

    private void OnDisable()
    {
        GunEvents.OnSetBlade -= SetBlade;
    }

    private void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass target = other.GetComponent<LivingAbstractClass>();
        if (target != null)
        {
            target.TakeDamage(Damage);
        }
    }

    void SetBlade(bool value)
    {
        Blade.enabled = value;
    }

    
}
