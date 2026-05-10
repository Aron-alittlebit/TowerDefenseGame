using UnityEngine;

public class Crystal : MonoBehaviour
{
    private int health = 100;
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
