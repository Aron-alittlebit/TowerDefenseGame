using System.Collections;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    [SerializeField] int NumberOfEntities;
    int EntitesToSpawn;
    [SerializeField] Entity Entity;
    [SerializeField] float TimeToSpawn;
    public static bool CanSpawn;
    public static int NumberOfAllEntities; 
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
            pos.y = 5;

            Entity entity = Instantiate(Entity, pos, transform.rotation);
            EntitesToSpawn--;
            
            yield return new WaitForSeconds(TimeToSpawn);
        }
    }


}
