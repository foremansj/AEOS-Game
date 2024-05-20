using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> obstaclePrefabs;
    [SerializeField] float spawningCountdown;
    [SerializeField] float timeBetweenSpawns;
    bool hasSpawnedBefore = false;
    public bool obstaclesAreSpawning = false;
    
    private void Update()
    {
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        if(spawningCountdown > Mathf.Epsilon)
        {
            spawningCountdown -= Time.deltaTime;
        }

        else if(spawningCountdown <= Mathf.Epsilon && !hasSpawnedBefore)
        {
            obstaclesAreSpawning = true;
            hasSpawnedBefore = true;
            StartCoroutine(SpawnObstacle());
        }

        else {
            return;
        }
    }

    IEnumerator SpawnObstacle(){
        while(obstaclesAreSpawning){
            GameObject newObstacle = Instantiate(RandomObstacle(), this.transform);
            newObstacle.transform.position = RandomizeSpawnPoint();
            //newObstacle.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            float randomSize = Random.Range(1.5f,2f);
            newObstacle.transform.localScale = Vector2.one * randomSize;
            yield return new WaitForSeconds(timeBetweenSpawns);}
    }

    private Vector2 RandomizeSpawnPoint(){
        int randomInt = Random.Range(0,spawnPoints.Count);
        Vector2 spawnPoint = spawnPoints[randomInt].position;
        return spawnPoint;
    }

    public void ChangeSpawningStatus(){
        obstaclesAreSpawning = !obstaclesAreSpawning;
    }

    private GameObject RandomObstacle(){
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
        return obstaclePrefabs[obstacleIndex];
    }
}
