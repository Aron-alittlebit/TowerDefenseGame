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
    //[SerializeField] TextMeshProUGUI ToFightingPhaseText;
    
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
        Debug.Log($"{CurrentPhase}, {LeftOfWaves}");
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
            Timer = BuildingPhasePeriod;
        }
    }

    
}

public enum Phase
{
    BuildingPhase,
    FightPhase
}
