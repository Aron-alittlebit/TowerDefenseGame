using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BannerBoost : MonoBehaviour
{
    List<Tower> boostedTowers;
    [SerializeField] int range;
    [SerializeField] float boostRate;
    [SerializeField] LayerMask ally;

    private void Start()
    {
        boostedTowers = new();
    }

    private void OnDisable()
    {
        DeBoostAllTowers();
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,range, ally);
        foreach(var col in colliders)
        {
            Tower tower = col.GetComponent<Tower>();
            if(tower != null && !boostedTowers.Contains(tower))
            {
                boostedTowers.Add(tower);
                tower.BoostedTower(boostRate);
            }
        }
    }

    void DeBoostAllTowers()
    {
        foreach(var tower in boostedTowers)
        {
            if(tower != null)
                tower.Deboost();
        }
        boostedTowers.Clear();
    }
}
