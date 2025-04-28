using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float minSpawnInterval = 1f;
    public float maxSpaenInterval = 3f;

    public int minEnemiesPerSpawn = 1;
    public int maxEnemiesPerSpawn = 3;

    public float spawnMargin = 1f;

    private float spawnRangeX;  
    private float spawnRangeY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSpawnRangeFromCamera();   
        StartCoroutine(SpawnEnemyCoroutine());
    }

    void SetSpawnRangeFromCamera()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        spawnRangeX = (width / 2f) + spawnMargin;
        spawnRangeY = (height / 2f) + spawnMargin;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        SpawnEnemies();

        while (true)
        {
            float interval = Random.Range(minSpawnInterval, maxSpaenInterval);
            yield return new WaitForSeconds(interval);

            SpawnEnemies();
        }
    }

    void SpawnEnemies()
    { 
        int enemiseToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn + 1);

        for (int i = 0; i < enemiseToSpawn; i++)
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[index];

            Vector3 camPos = Camera.main.transform.position;

            Vector2 spwanPos = new Vector2(
                Random.Range(camPos.x - spawnRangeX, camPos.x + spawnRangeX),
                Random.Range(camPos.y - spawnRangeY, camPos.y + spawnRangeY));


            Instantiate(enemyPrefab, spwanPos, Quaternion.identity);
         }
    }
}
