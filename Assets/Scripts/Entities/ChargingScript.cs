using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class ChargingScript : EntityMove
{
    bool IsInCharge;
    float originalSpeed;
    [SerializeField] float ChargingCoolDown;
    [SerializeField] float ChargingSpeed;
    float ChargingCurrentCoolDown;
    string action;
    bool IsAttacking;
    bool hasAttacked;
    
    

    protected override void Start()
    {
        base.Start();
        IsInCharge = true;
        IsAttacking = false;
        originalSpeed = speed;
        ChargingCurrentCoolDown = ChargingCoolDown;
        hasAttacked = false;
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

        if(!IsAttacking)
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
            hasAttacked = false;
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
                IsInCharge = false; 
                animator.SetBool(action, true);
                MoveTowardsWayPoints();
                //Debug.Log($"Moving to waypoints");
            }
            else if (dst <= attackDst)
            {

                if (IsInCharge)
                {
                    EntitiesEvent.ChargeStateChanged(true, 
                        GetComponentInChildren<ChargingAttackScript>().gameObject);
                    hasAttacked = true; 
                }

                IsAttacking = true;
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
                Turn(Target.transform.position);
                EntitiesEvent.EntityAttack(gameObject);

                //Debug.Log($"Attacking target");
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

    public void ResetChargeStateFromWeapon()
    {
        IsInCharge = false;
        ChargingCurrentCoolDown = ChargingCoolDown;
        EntitiesEvent.ChargeStateChanged(false, 
            GetComponentInChildren<ChargingAttackScript>().gameObject);
    }


}
