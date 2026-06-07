using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : LivingAbstractClass
{
    [SerializeField] Transform SpawnPoint;

    protected override void Start()
    {
        base.Start();
        transform.position = SpawnPoint.position;
    }
    protected override void Die()
    {
        if (health <= 0)
        {

            health = StartingHealth;
            transform.position = SpawnPoint.position;

        }
    }
}
