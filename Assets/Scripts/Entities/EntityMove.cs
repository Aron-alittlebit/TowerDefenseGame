using NUnit.Framework.Internal.Builders;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    [SerializeField] Crystal Crystal;
    [SerializeField] float speed = 10f;
    [SerializeField] float attackDst = 6f;
    float cooldown = 1;

    private void Start()
    {
        Crystal = FindAnyObjectByType<Crystal>();
        if (Crystal == null) Debug.LogError("No Crystal found in scene!", this);
        else transform.LookAt(Crystal.transform);
    }

    void Update()
    {

        transform.LookAt(Crystal.transform);
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
