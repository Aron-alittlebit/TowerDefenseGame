using NUnit.Framework;
using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityMove : MonoBehaviour
{
    protected Crystal Crystal;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float attackDst;
    [SerializeField] protected float Range;
    [SerializeField] protected LayerMask Ally;
    protected List<Vector3> path = new List<Vector3>();
    protected int indexer = 0;
    protected Animator animator;
    protected bool AllyNearby;
    protected LivingAbstractClass Target = null;
    protected float MinDst = float.MaxValue;
    protected bool isDead;


    protected virtual void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        

        Crystal = FindAnyObjectByType<Crystal>();
        if (Crystal == null) Debug.LogError("No Crystal found in scene!", this);
        else transform.LookAt(Crystal.transform);
    }

    protected virtual void OnEnable()
    {

        EntitiesEvent.OnEntityDeath += EntityDied;
        EntitiesEvent.OnEntityRevived += RevivingEntity;
    }

    protected virtual void OnDisable()
    {

        EntitiesEvent.OnEntityDeath -= EntityDied;
        EntitiesEvent.OnEntityRevived -= RevivingEntity;
    }

    protected virtual void Update()
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
                MoveTowardsWayPoints();
                Debug.Log($"Moving to waypoints");
            }
            else if (dst <= attackDst)
            {
                Turn(Target.transform.position);
                animator.SetBool("Walk", false);
                EntitiesEvent.EntityAttack(gameObject);
                Debug.Log("Attacking target");
            }
            else
            {
                animator.SetBool("Walk", true);
                Turn(Target.transform.position);
                Vector3 newPos = Target.transform.position;
                newPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position,
                newPos, speed * Time.deltaTime);
                Debug.Log($"Walking towards target {dst}");

            }

        }
        else
        {
            //MinDst = float.MaxValue;
            //Target = null;
            animator.SetBool("Walk", true);
            MoveTowardsWayPoints();
            Debug.Log($"walking towards waypoints");
        }
    }
    


    public void SetPath(List<Vector3> GivenPath, Crystal crystal)
    {
        indexer = 0;
        path.Clear();
        path = GivenPath;
        Crystal = crystal;
    }

    protected void MoveTowardsWayPoints()
    {
        
        if(indexer < path.Count)
        {
            Turn(path[indexer]);
            if (Vector3.Distance(transform.position, path[indexer]) >= 0.1)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                path[indexer], speed * Time.deltaTime);
            }
            else
            {
                indexer++;
            }
        }
        else
        {
            MoveTowardsCrystal();
        }

    }

    protected void MoveTowardsCrystal()
    {
        Turn(Crystal.transform.position);

        if (Vector3.Distance(transform.position, Crystal.transform.position) >= attackDst * 2)
        {
            Vector3 newPos = Crystal.transform.position;
            newPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position,
            newPos, speed * Time.deltaTime);
        }
        //else
        //{
        //    animator.SetBool("Walk", false);
        //    EntitiesEvent.EntityAttack(gameObject);
        //}
    }

    protected void Turn(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation,
            targetRotation, Time.deltaTime * 5);

        }
    }

    protected void EntityDied(int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            EntityAttack attack = transform.GetComponent<EntityAttack>();
            attack.enabled = false;
            isDead = true;

        }

    }

    protected void RevivingEntity(int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            EntityAttack attack = transform.GetComponent<EntityAttack>();
            attack.enabled = true;
            isDead = false;

        }

    }





}
