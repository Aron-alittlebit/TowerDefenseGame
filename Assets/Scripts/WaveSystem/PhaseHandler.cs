using System.Collections;
using TMPro;
using UnityEngine;

public class PhaseHandler : MonoBehaviour
{
    [SerializeField] int NumberOfWaves;
    public static int LeftOfWaves;
    public Phase CurrentPhase;
    float BuildingPhasePeriod = 30;
    float Timer;
    bool waveCompleted = false;
    
    
    
    void Start()
    {
        
        LeftOfWaves = NumberOfWaves;
        Timer = BuildingPhasePeriod;
        CurrentPhase = Phase.BuildingPhase;
    }

    
    void Update()
    {
       
        if (NumberOfWaves <= 0 || LeftOfWaves <= 0) return;
        CompletedWave();
        Timer -= Time.deltaTime;
        if(CurrentPhase == Phase.BuildingPhase)
        {
            SpawnEntities.CanSpawn = false;
            if(Input.GetKeyDown(KeyCode.G) || Timer <= 0)
            {
                waveCompleted = false;
                CurrentPhase = Phase.FightPhase;
                SpawnEntities.CanSpawn = true;
                EntitiesEvent.StartSpawning();
            }
        }
    }

    void CompletedWave()
    {
        if (SpawnEntities.NumberOfAllEntities <= 0 
            && !waveCompleted && CurrentPhase == Phase.FightPhase)
        {
            waveCompleted = true;
            LeftOfWaves -= 1;
            CurrentPhase = Phase.BuildingPhase;

            if(LeftOfWaves == NumberOfWaves)
                BuildingPhasePeriod = 90;
            else
                BuildingPhasePeriod = 30;

            Timer = BuildingPhasePeriod;

            //Gives player gems after each wave
            if(LeftOfWaves > 0)
            {
                TowerEvents.TowerSold(10);
            }
        }
    }

    
}

public enum Phase
{
    BuildingPhase,
    FightPhase
}
