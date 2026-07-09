using UnityEngine;

public class ChargingAttackScript : EntityWeaponColliderScript
{
    [SerializeField] int ChargeImpactDamage;
    bool charge;

    protected override void Start()
    {
        base.Start();
        
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

            HasHit = false;

        }
    }

    void ChargeState(bool state, GameObject sender)
    {
        Debug.Log(charge);
        if (sender!= gameObject) return;

        charge = state;
    }
}
