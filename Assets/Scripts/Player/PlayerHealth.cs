using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : LivingAbstractClass
{
    protected override void Die()
    {
        if (health <= 0)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
