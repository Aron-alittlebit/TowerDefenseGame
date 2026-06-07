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
    

    private void Start()
    {
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
        if (Physics.Raycast(transform.position, 
            transform.forward, out RaycastHit hitInfo, attackDst, Ally))
        {

            LivingAbstractClass ally = hitInfo.collider.GetComponent<LivingAbstractClass>();

            EntitiesEvent.EntityAttack(ally);
                
        }
        else
        {
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

            transform.position = Vector3.MoveTowards(transform.position,
            Crystal.transform.position, speed * Time.deltaTime);
        }
        else
        {
           EntitiesEvent.EntityAttack(Crystal);
        }
    }

    
}
