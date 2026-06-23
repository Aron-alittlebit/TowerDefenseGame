using NUnit.Framework;
using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EntityMove : MonoBehaviour
{
    [SerializeField] Crystal Crystal;
    [SerializeField] float speed = 10f;
    [SerializeField] float attackDst = 6f;
    [SerializeField] float Range;
    [SerializeField] LayerMask Ally;
    List<Vector3> path = new List<Vector3>();
    int indexer = 0;
    Animator animator;
    bool AllyNearby;
    LivingAbstractClass Target = null;
    float MinDst = float.MaxValue;


    private void Start()
    {
        
        animator = GetComponent<Animator>();
        Crystal = FindAnyObjectByType<Crystal>();
        if (Crystal == null) Debug.LogError("No Crystal found in scene!", this);
        else transform.LookAt(Crystal.transform);
    }

    private void OnEnable()
    {
        
        EntitiesEvent.OnEntityDeath += SetSpeedToZero;
    }

    private void OnDisable()
    {
        
        EntitiesEvent.OnEntityDeath -= SetSpeedToZero;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, Ally);
        AllyNearby = colliders.Length > 0;

        if (AllyNearby)
        {
            foreach (var collider in colliders)
            {
                if (Vector3.Distance(transform.position, collider.transform.position) <= MinDst)
                {
                    Target = collider.GetComponent<LivingAbstractClass>();
                    MinDst = Vector3.Distance(transform.position, collider.transform.position);
                }
            }
        }

        if (Target != null)
        {
            
            bool validTarget = Target.GetComponent<LivingAbstractClass>() != null
                || Target.GetComponent<Tower>().IsBuilt;
            float dst = Vector3.Distance(transform.position, Target.transform.position);

            

            if (!validTarget || dst > attackDst)
            {
                MinDst = float.MaxValue;
                Target = null;
                animator.SetBool("Walk", true);
                MoveTowardsWayPoints();
                //Debug.Log($"1 {validTarget}");
            }
            else if (dst <= attackDst)
            {
                Turn(Target.transform.position);
                animator.SetBool("Walk", false);
                EntitiesEvent.EntityAttack(Target, gameObject);
                //Debug.Log("2");
            }
            else
            {
                animator.SetBool("Walk", true);
                Vector3 newPos = Target.transform.position;
                newPos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position,
                newPos, speed * Time.deltaTime);
                //Debug.Log("3");

            }

        }
        else
        {
            MinDst = float.MaxValue;
            Target = null;
            animator.SetBool("Walk", true);
            MoveTowardsWayPoints();
            //Debug.Log("4");
        }
    }
    


    public void SetPath(List<Vector3> GivenPath)
    {
        indexer = 0;
        path.Clear();
        path = GivenPath;
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
            MoveTowardsCrystal();
        }
            
    }

    void MoveTowardsCrystal()
    {
        Turn(Crystal.transform.position);
        

        if (Vector3.Distance(transform.position, Crystal.transform.position) > attackDst)
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

    void SetSpeedToZero(int id)
    {
        if(id == transform.GetInstanceID())
            speed = 0f; 
    }

    
}
