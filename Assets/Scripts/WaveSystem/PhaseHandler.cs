using System.Collections;
using TMPro;
using UnityEngine;

public class PhaseHandler : MonoBehaviour
{
    [SerializeField] int NumberOfWaves;
    public static int LeftOfWaves;
    public Phase CurrentPhase;
    
    float Timer;
    bool waveCompleted;
    [SerializeField] AudioClip BattleClip;
    [SerializeField] AudioClip RestBetweenWaves;


    void Start()
    {
        waveCompleted = false;
        LeftOfWaves = NumberOfWaves;
        Timer = 360;
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
            
            if (Input.GetKeyDown(KeyCode.G) || Timer <= 0)
            {
                SoundManager.instance.ChangeMusic(BattleClip);
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
            SoundManager.instance.ChangeMusic(RestBetweenWaves);
            waveCompleted = true;
            
            CurrentPhase = Phase.BuildingPhase;

            if(LeftOfWaves <= NumberOfWaves)
                Timer = 90;

            LeftOfWaves -= 1;
            //Gives player gems after each wave
            if (LeftOfWaves > 0)
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
