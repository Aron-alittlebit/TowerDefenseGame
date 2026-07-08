using NUnit.Framework;
using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityMove : MonoBehaviour
{
    Crystal Crystal;
    [SerializeField] float speed = 10f;
    [SerializeField] float attackDst;
    [SerializeField] float Range;
    [SerializeField] LayerMask Ally;
    List<Vector3> path = new List<Vector3>();
    int indexer = 0;
    Animator animator;
    bool AllyNearby;
    LivingAbstractClass Target = null;
    float MinDst = float.MaxValue;
    bool isDead;


    private void Start()
    {
        isDead = false;
        animator = GetComponent<Animator>();
        

        Crystal = FindAnyObjectByType<Crystal>();
        if (Crystal == null) Debug.LogError("No Crystal found in scene!", this);
        else transform.LookAt(Crystal.transform);
    }

    private void OnEnable()
    {

        EntitiesEvent.OnEntityDeath += EntityDied;
        EntitiesEvent.OnEntityRevived += RevivingEntity;
    }

    private void OnDisable()
    {

        EntitiesEvent.OnEntityDeath -= EntityDied;
        EntitiesEvent.OnEntityRevived -= RevivingEntity;
    }

    void Update()
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

        

        if (Target != null && Target != Crystal)
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
                EntitiesEvent.EntityAttack(Target, gameObject);
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

    void MoveTowardsWayPoints()
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
            //Target = Crystal;
            MoveTowardsCrystal();
        }
            
    }

    void MoveTowardsCrystal()
    {
        Turn(Crystal.transform.position);

        //Debug.Log(Vector3.Distance(transform.position, Crystal.transform.position));

        if (Vector3.Distance(transform.position, Crystal.transform.position) >= attackDst*2)
        {
            Vector3 newPos = Crystal.transform.position;
            newPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position,
            newPos, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
            EntitiesEvent.EntityAttack(Crystal, gameObject);
        }
    }

    void Turn(Vector3 target)
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

    void EntityDied(int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            EntityAttack attack = transform.GetComponent<EntityAttack>();
            attack.enabled = false;
            isDead = true;

        }

    }

    void RevivingEntity(int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            EntityAttack attack = transform.GetComponent<EntityAttack>();
            attack.enabled = true;
            isDead = false;

        }

    }





}
