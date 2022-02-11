using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs of objects")]
    public GameObject enemyPrefab;
    public GameObject[] specialEnemyPrefabs;
    public GameObject powerUpPrefab;
    public int enemyCount;
    private float spawnRange = 9;
    private bool isRoundOver = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver) HandleWaves();
    }

    // Checks if the previous round is over, if it is, it spawns the next wave of enemies and power ups
    private void HandleWaves()
    {
        if (!GameManager.instance.isRoundOver && isRoundOver)
        {
            SpawnEnemyWave(GameManager.instance.round);
            //SpawnPowerUpWave(GameManager.instance.round - 1);
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            isRoundOver = false;
        }

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount <= 0 && !GameManager.instance.isRoundOver)
        {
            DeleteAllPowerUps();
            isRoundOver = true;
            GameManager.instance.RoundOver();
        }
    }

    // Generates a random position on the map
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void DeleteAllPowerUps()
    {
        GameObject[] leftovers = GameObject.FindGameObjectsWithTag("PowerUp");
        foreach(GameObject powerUp in leftovers)
        {
            Destroy(powerUp);
        }
    }

    // Deactivated for balance purposes
    /*private void SpawnPowerUpWave(int numOfPowerUps)
    {
        for (int i = 0; i < numOfPowerUps; i++)
        {
            Instantiate(powerUpPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }*/

    void SpawnEnemyWave(int numOfEnemies)
    {
        if (GameManager.instance.round >= 5 && GameManager.instance.round  < 10)
        {
            Instantiate(specialEnemyPrefabs[0], GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            numOfEnemies--;
        } 
        
        if (GameManager.instance.round >= 10)
        {
            Instantiate(specialEnemyPrefabs[0], GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            Instantiate(specialEnemyPrefabs[1], GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            numOfEnemies -= 2;
        }

        for (int i = 0; i < numOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
