using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    [SerializeField] int NumberOfEntities;
    int EntitesToSpawn;
    [SerializeField] Entity Entity;
    [SerializeField] float TimeToSpawn;
    public static bool CanSpawn;
    public static int NumberOfAllEntities;
    [SerializeField] GameObject path;
    void Start()
    {
        CanSpawn = false;
        EntitesToSpawn = NumberOfEntities;
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
        NumberOfAllEntities += NumberOfEntities;
        EntitesToSpawn = NumberOfEntities;
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

        while (EntitesToSpawn > 0) 
        {
            int randomSide = Random.Range(-10, 10);
            Vector3 pos = transform.position + transform.right * randomSide;
            pos.y = transform.position.y;

            Entity entity = Instantiate(Entity, pos, transform.rotation);
            EntitiesEvent.SetPath(GeneratePath());
            EntitesToSpawn--;
            
            yield return new WaitForSeconds(TimeToSpawn);
        }
    }

    List<Vector3> GeneratePath()
    {
        List<Vector3> waypoints = new List<Vector3>();

        foreach(Transform child in path.GetComponentInChildren<Transform>())
        {
            Vector3 newPos = child.transform.position;
            newPos.y = transform.position.y;
            child.transform.position = newPos;
            waypoints.Add(child.transform.position);
        }

        return waypoints;
    }


}
