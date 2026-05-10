using System.Collections;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    private void OnEnable()
    {
        EntitiesEvent.OnEntityReadyToAttack += Attack;
    }
    private void OnDisable()
    {
        EntitiesEvent.OnEntityReadyToAttack -= Attack;
    }
    void Attack()
    {
        Debug.Log("Die you stupid brick");
    }

    
}
