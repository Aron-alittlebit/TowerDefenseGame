using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MoleWeaponColliderScript : EntityWeaponColliderScript
{
    [SerializeField] float StunTimer;
    
    Dictionary<LivingAbstractClass, float> stunnedTowers;
    List<LivingAbstractClass> expiredTowers;

    protected override void Start()
    {
        base.Start();
        
        stunnedTowers = new Dictionary<LivingAbstractClass, float>();
        expiredTowers = new List<LivingAbstractClass>();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        LivingAbstractClass enemy = other.GetComponent<LivingAbstractClass>();


        if (enemy != null && enemy.Health > 0 && HasHit && !enemy.GetComponent<Entity>())
        {
            if (enemy.GetComponent<Tower>() 
                && !stunnedTowers.ContainsKey(enemy))
            {
                enemy.enabled = false;
                stunnedTowers.Add(enemy, Time.time+StunTimer);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
            
            HasHit = false;

        }
    }

    private void Update()
    {
        foreach(var tower in stunnedTowers)
        {
            if(Time.time >= tower.Value)
            {
                expiredTowers.Add(tower.Key);
            }
        }

        foreach (var tower in expiredTowers)
        {
            if (tower != null)
            {
                tower.enabled = true;
            }
            stunnedTowers.Remove(tower);
        }

        expiredTowers.Clear();
    }
}
