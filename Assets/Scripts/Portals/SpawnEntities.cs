using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public struct SpawnData
{
    public Entity entity;
    public int amount;
}

public class SpawnEntities : MonoBehaviour
{
    //[SerializeField] int NumberOfEntities;
    [SerializeField] List<SpawnData> EntitiesList;
    int EntitiesToSpawn;
    //[SerializeField] Entity Entity;
    [SerializeField] float TimeToSpawn;
    public static bool CanSpawn;
    public static int NumberOfAllEntities;
    [SerializeField] GameObject path;
    [SerializeField] Crystal Crystal;
    [SerializeField] Transform SpawnPoint;
    void Start()
    {
        CanSpawn = false;
        
        //NumberOfAllEntities += NumberOfEntities;
    }

    private void OnEnable()
    {
        EntitiesEvent.OnEntityDeath += SubtractFromAllEntites;
        EntitiesEvent.OnStartSpawning += StartSpawn;
    }

    private void OnDisable()
    {
        EntitiesEvent.OnEntityDeath -= SubtractFromAllEntites;
        EntitiesEvent.OnStartSpawning -= StartSpawn;
    }
    void StartSpawn()
    {
        if (!CanSpawn) return;
        NumberOfAllEntities += EntitiesList.Sum(x => x.amount);
        
        StartCoroutine(SpawnLogic());

    }

    //private void Update()
    //{
    //    Debug.Log($"{NumberOfAllEntities}");
    //}

    void SubtractFromAllEntites(int id)
    {
        NumberOfAllEntities -= 1;
    }


    IEnumerator SpawnLogic()
    {
        for(int i = 0; i < EntitiesList.Count; i++)
        {
            EntitiesToSpawn = EntitiesList[i].amount;
            while (EntitiesToSpawn > 0)
            {
                int randomSide = UnityEngine.Random.Range(-10, 10);
                Vector3 pos = transform.position + transform.right * randomSide;
                pos.y = SpawnPoint.position.y;


                Entity entity = Instantiate(EntitiesList[i].entity, pos, transform.rotation);

                entity.GetComponent<EntityMove>().SetPath(GeneratePath(), Crystal);
                EntitiesToSpawn--;

                yield return new WaitForSeconds(TimeToSpawn);
            }
        }
        
    }

    List<Vector3> GeneratePath()
    {
        List<Vector3> waypoints = new List<Vector3>();
        
        foreach(Transform child in path.transform)
        {
            waypoints.Add(child.position);
        }

        return waypoints;
    }


}
