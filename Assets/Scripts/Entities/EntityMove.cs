using NUnit.Framework.Internal.Builders;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    [SerializeField] Crystal Crystal;
    float speed = 10f;
    float attackDst = 6f;
    float cooldown = 1;

   

    void Update()
    {

        
        if (Vector3.Distance(transform.position, Crystal.transform.position) >= attackDst)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            Crystal.transform.position, speed * Time.deltaTime);
        }
        else
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                EntitiesEvent.EntityAttack();
                cooldown = 1;
            }
                
        }
    }
}
