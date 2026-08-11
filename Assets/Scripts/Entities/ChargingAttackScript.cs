using UnityEngine;

public class ChargingAttackScript : EntityWeaponColliderScript
{
    [SerializeField] int ChargeImpactDamage;
    bool charge;

    protected override void Start()
    {
        base.Start();    
        charge = true;
    }

    protected virtual void OnEnable()
    {
        EntitiesEvent.OnChargeStateChanged += ChargeState;
    }
    protected virtual void OnDisable()
    {
        EntitiesEvent.OnChargeStateChanged -= ChargeState;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass enemy = other.GetComponent<LivingAbstractClass>();


        if (enemy != null && enemy.Health > 0 && HasHit && !enemy.GetComponent<Entity>())
        {
            enemy.TakeDamage(charge ? ChargeImpactDamage : Damage);
            
            if (charge)
            {
                charge = false;
                GetComponentInParent<ChargingScript>().ResetChargeStateFromWeapon();
            }
            HasHit = false;

        }
    }

    public void ChargeState(bool state, GameObject sender)
    {
        
        if (sender != gameObject) return;

        charge = state;
    }
}
