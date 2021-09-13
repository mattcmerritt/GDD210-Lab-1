using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static float MaxPosition = 22, StartPosition = 22, StartHeight = 2;
    public int MaxEnemyCount;
    public GameObject Trigger;
    public float EnemyTimer, InitialEnemyTimer, MinEnemyTimer;
    public int EnemiesSpawned;
    public float DifficultyIncrease;
    public GameObject EnemyPrefab;

    private void Update()
    {
        EnemyTimer -= Time.deltaTime;
        
        // spawn a new enemy if it is timeand if there is space
        if (EnemyTimer <= 0 && GameObject.FindGameObjectsWithTag("Enemy").Length < MaxEnemyCount)
        {
            // sets up the timer for the next enemy, making it a little bit faster for every enemy spawned
            EnemyTimer = InitialEnemyTimer - Random.Range(0, Mathf.Clamp(DifficultyIncrease * EnemiesSpawned, 0, MinEnemyTimer));
            // creating the enemy with a prefab
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-MaxPosition, MaxPosition), 2, StartPosition), Quaternion.identity);
            EnemiesSpawned++;
        }
    }
}
