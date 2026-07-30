using UnityEngine;

public class MoleMove : EntityMove
{
    [SerializeField] float BurrowSpeed;
    [SerializeField] float BurrowCoolDown;
    [SerializeField] float BurrowDuration;
    float currentBurrowCoolDown;
    float currentBurrowDuration;
    burrowEffect burrowEffect;
    [SerializeField] private ParticleSystem dirtParticles;

    protected override void Start()
    {
        base.Start();
        currentBurrowCoolDown = 0;
        currentBurrowDuration = BurrowDuration;
        burrowEffect = GetComponent<burrowEffect>();
    }

    protected override void Update()
    {

        if (isDead) return;
        
        PlayWalkingSound();
        WalkingTimer -= Time.deltaTime;
        dirtParticles.transform.position = new Vector3(transform.position.x,0,transform.position.z);


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
        
        if (burrowEffect.IsBurrowing)
        {
            currentBurrowDuration -= Time.deltaTime;
        }
        else
        {
            currentBurrowCoolDown -= Time.deltaTime;
        }

        if(currentBurrowDuration <= 0f && burrowEffect.IsBurrowing)
        {
            
            burrowEffect.ToggleBurrow();
            currentBurrowDuration = BurrowDuration;
            dirtParticles.Stop();
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
                animator.SetBool("Walk", true);
                IsWalking = true;
                MoveTowardsWayPoints();
                //Debug.Log($"Moving to waypoints");
            }
            else if (dst <= attackDst)
            {
                Turn(Target.transform.position);

                animator.SetBool("Walk", false);
                IsWalking = false;
                EntitiesEvent.EntityAttack(gameObject);
                //Debug.Log("Attacking target");
            }
            else
            {
               
                if(currentBurrowCoolDown <= 0)
                {
                    
                    burrowEffect.ToggleBurrow();
                    animator.SetTrigger("Burrow");
                    currentBurrowCoolDown = BurrowCoolDown;
                    dirtParticles.Play();
                    

                }
                else if(!burrowEffect.IsBurrowing)
                {
                    
                    animator.SetBool("Walk", true);
                    IsWalking = true;
                    Turn(Target.transform.position);
                }
                
                Vector3 newPos = Target.transform.position;
                newPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position,
                newPos, speed * Time.deltaTime);
                //Debug.Log($"Walking towards target {dst}");

            }

        }
        else
        {
            IsWalking = true;
            animator.SetBool("Walk", true);
            MoveTowardsWayPoints();
            //Debug.Log($"walking towards waypoints");
        }

    }

}
