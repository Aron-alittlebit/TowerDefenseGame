using System.Collections;
using UnityEngine;

public class ChargingScript : EntityMove
{
    public bool IsInCharge { get; private set; }
    float originalSpeed;
    [SerializeField] float ChargingCoolDown;
    [SerializeField] float ChargingSpeed;
    float ChargingCurrentCoolDown;
    string action;
    bool IsAttacking;
    bool HasStartedAttack;
    

    protected override void Start()
    {
        base.Start();
        IsInCharge = true;
        IsAttacking = false;
        HasStartedAttack = false;
        originalSpeed = speed;
        ChargingCurrentCoolDown = ChargingCoolDown;
    }

    protected override void Update()
    {

        if (isDead) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, Ally);
        AllyNearby = colliders.Length > 0;

        if (AllyNearby)
        {
            foreach (var collider in colliders)
            {
                if (Vector3.Distance(transform.position, collider.transform.position)
                    <= MinDst)
                {
                    Target = collider.GetComponent<LivingAbstractClass>();
                    MinDst = Vector3.Distance(transform.position,
                        collider.transform.position);
                }
            }
        }

        ChargingCurrentCoolDown -= Time.deltaTime;
        if (IsInCharge)
        {
            action = "Run";
            speed = ChargingSpeed;
        }
        else
        {
            action = "Walk";
            speed = originalSpeed;
        }

        if (!IsInCharge && !IsAttacking && ChargingCurrentCoolDown <= 0)
        {
            IsInCharge = true;
            ChargingCurrentCoolDown = ChargingCoolDown;
            
        }


        if (Target != null)
        {

            bool validTarget = Target.GetComponent<LivingAbstractClass>() != null
                || Target.GetComponent<Tower>().IsBuilt;
            float dst = Vector3.Distance(transform.position, Target.transform.position);
            


            if (!validTarget || dst > Range)
            {
                MinDst = float.MaxValue;
                Target = null;
                IsAttacking = false;
                animator.SetBool(action, true);
                MoveTowardsWayPoints();
                //Debug.Log($"Moving to waypoints");
            }
            else if (dst <= attackDst)
            {
                if (!IsAttacking && !HasStartedAttack)
                {
                    EntitiesEvent.ChargeStateChanged(IsInCharge,
                        GetComponentInChildren<ChargingAttackScript>().gameObject);
                    IsInCharge = false;
                    IsAttacking = true;
                    HasStartedAttack = true;
                    ChargingCurrentCoolDown = ChargingCoolDown;
                    animator.SetBool(action, false);
                }
                Turn(Target.transform.position);
                EntitiesEvent.EntityAttack(Target, gameObject);
            }
            else
            {
                IsAttacking = false;
                Turn(Target.transform.position);
                animator.SetBool(action, true);
                Vector3 newPos = Target.transform.position;
                newPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position,
                newPos, speed * Time.deltaTime);
                //Debug.Log($"Walking towards target");

            }

        }
        else
        {
            IsAttacking = false;
            animator.SetBool(action, true);
            MoveTowardsWayPoints();
            //Debug.Log($"walking towards waypoints");
        }
    }

    
}
