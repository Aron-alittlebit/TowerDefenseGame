using NUnit.Framework;
using NUnit.Framework.Internal.Builders;
using System.Collections.Generic;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    [SerializeField] Crystal Crystal;
    [SerializeField] float speed = 10f;
    [SerializeField] float attackDst = 6f;
    [SerializeField] LayerMask Ally;
    List<Vector3> path = new List<Vector3>();
    bool HasReachedWayPoint = false;
    int indexer = 0;
    Animator animator;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        Crystal = FindAnyObjectByType<Crystal>();
        if (Crystal == null) Debug.LogError("No Crystal found in scene!", this);
        else transform.LookAt(Crystal.transform);
    }

    private void OnEnable()
    {
        EntitiesEvent.OnSetPath += SetPath;
    }

    private void OnDisable()
    {
        EntitiesEvent.OnSetPath -= SetPath;
    }

    void Update()
    {
        Debug.Log(transform.position.y);
        if (Physics.Raycast(transform.position, 
            transform.forward, out RaycastHit hitInfo, attackDst, Ally))
        {

            LivingAbstractClass ally = hitInfo.collider.GetComponent<LivingAbstractClass>();
            animator.SetBool("Walk", false);
            EntitiesEvent.EntityAttack(ally);
                
        }
        else
        {
            animator.SetBool("Walk", true);
            MoveTowardsWayPoints();
        }
        
    }

    void SetPath(List<Vector3> GivenPath)
    {
        path.Clear();
        path = GivenPath;
    }

    void MoveTowardsWayPoints()
    {
        if(indexer < path.Count)
        {
            transform.LookAt(path[indexer]);
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
        transform.LookAt(Crystal.transform);
        if (Vector3.Distance(transform.position, Crystal.transform.position) >= attackDst)
        {
            Vector3 newPos = Crystal.transform.position;
            newPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position,
            newPos, speed * Time.deltaTime);
        }
        else
        {
           EntitiesEvent.EntityAttack(Crystal);
        }
    }

    
}
