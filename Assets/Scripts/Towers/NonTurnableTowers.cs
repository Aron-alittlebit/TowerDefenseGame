using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class NonTurnableTowers : MonoBehaviour
{
    [SerializeField] TowerData towerData;
    float Range;

    private void Start()
    {
        Range = towerData.Range;
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range,
            Tower.Instance.EntityLayer);
        foreach (var collider in colliders)
        {
            GunEvents.TowerAttack(towerData, gameObject);
            break;

        }
    }
}
