using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    [SerializeField] GameObject enemy1;
    [SerializeField] Transform enemyTarget;

    [SerializeField] int enemiesToSpawn;
    [SerializeField] int spawnedEnemies = 0;
    bool allSpawned = false;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    bool allDead = false;
    

    public Action WaveFinished;


    

    void Start()
    {
        
    }


    void Update()
    {
       
    }

    IEnumerator Spawn(GameObject enemyToSpawn)
    {
        while (spawnedEnemies < enemiesToSpawn)
        {
            yield return new WaitForSeconds(1.5f);
            GameObject enemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            enemy.transform.Translate(new Vector3(UnityEngine.Random.Range(-1f, 1f),UnityEngine.Random.Range(-1f, 1f),0));
            enemy.GetComponent<EnemyMovement>().SetTarget(enemyTarget);
            enemy.GetComponent<Health>().Died += EnemyDied;
            enemies.Add(enemy);
            spawnedEnemies++;
            
            if (spawnedEnemies >= enemiesToSpawn) allSpawned = true;
        }
    }

    void EnemyDied(GameObject enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0 && allSpawned) 
        {
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1);
        WaveFinished();
    }


    public void StartWave(int level)
    {
        GenerateWave(level);
        StartCoroutine(Spawn(enemy1));
    }

    public void GenerateWave(int level)
    {
        spawnedEnemies = 0;
        enemiesToSpawn = level;
        allSpawned = false;
    }



}
