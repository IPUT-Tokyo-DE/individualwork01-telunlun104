using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BadEnemySpawner : MonoBehaviour
{
    public GameObject badEnemyPrefab;
    public float spawnInterval = 5f;
    [Range(0f, 1f)]
    public float spawnChance = 0.5f;

    public int minEnemiesPerSpawn = 1;
    public int maxEnemiesPerSpawn = 3;

    public float spawnMargin = 1f;

    private float spawnRangeX;
    private float spawnRangeY;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSpawnRangeFromCamera();
        StartCoroutine(SpawnBadEnemyCoroutine());
    }

    // Update is called once per frame
    void SetSpawnRangeFromCamera()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        spawnRangeX = (width / 2f) + spawnMargin;
        spawnRangeY = (height / 2f) + spawnMargin;
    }

    IEnumerator SpawnBadEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (Random.value < spawnChance)
            {
                int enemiesToSpawn = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn + 1);


                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    Vector3 camPos = Camera.main.transform.position;

                    Vector2 spawnPos = new Vector2(
                        Random.Range(camPos.x - spawnRangeX, camPos.x + spawnRangeX),
                        Random.Range(camPos.y - spawnRangeY, camPos.y + spawnRangeY));

                    Instantiate(badEnemyPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}


