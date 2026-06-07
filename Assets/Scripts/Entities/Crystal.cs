using UnityEngine;
using UnityEngine.SceneManagement;

public class Crystal : LivingAbstractClass
{
    public static int NumbersOfCrystals;

    private void Awake()
    {
        NumbersOfCrystals++;
    }

    protected override void Die()
    {
        if(health <= 0)
        {
            NumbersOfCrystals--;
            Destroy(gameObject);

            if(NumbersOfCrystals <= 0)
            {
                
            }
        }


    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerGemPickUp.GemCounter = PlayerGemPickUp.ReadOnlyStartingGem;
    }

}
