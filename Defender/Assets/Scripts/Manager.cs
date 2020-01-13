using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] Controller controller;
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] StatsUi ui;

    
    public Action StartLevel;
    public Action EndLevel;


    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);

        Stats stats = SavingSystem.Load();
        if (stats == null)
        {
            stats = new Stats();
        }
        controller.stats = stats;
    }

    void Start()
    {
        waveSpawner.WaveFinished += LevelEnd;
        ui.OnStartGamePressed += LevelStart;

        //StartLevel += waveSpawner.StartWave;
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SavingSystem.Save(controller.stats);

        if (Input.GetKeyDown(KeyCode.Alpha1)) StartLevel();
        if (Input.GetKeyDown(KeyCode.Alpha2)) EndLevel();
    }


    void LevelStart()
    {
        StartLevel();
        waveSpawner.StartWave(controller.stats.currentLevel);
    }

    void LevelEnd()
    {
        controller.stats.currentLevel++;
        EndLevel();
    }



}
