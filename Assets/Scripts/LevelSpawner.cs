using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] platformPrefabs;
    public float spawnDistance = 20f;
    public float platformLength = 10f;

    private Transform player;
    private float lastSpawnX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastSpawnX = player.position.x;
    }

    void Update()
    {
        if (player.position.x > lastSpawnX - spawnDistance)
        {
            SpawnPlatform();
            lastSpawnX += platformLength;
        }
    }

    void SpawnPlatform()
    {
        int index = Random.Range(0, platformPrefabs.Length);
        Vector3 spawnPosition = new Vector3(lastSpawnX + platformLength, 0f, 0f);
        Instantiate(platformPrefabs[index], spawnPosition, Quaternion.identity);
    }
}
