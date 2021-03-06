﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundTile : MonoBehaviour
{
    GroundSpawn gs;
    Vector3 nextSpawnPoint;
    public float secondsBetweenSpawn;
    public float elapsedTime = 0.0f;
    public GameObject pDiamondPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // On start - spawn ground and diamonds
        gs = GameObject.FindObjectOfType<GroundSpawn>();  
        SpawnDiamonds();  
    }

    private void OnTriggerExit (Collider other)
    {
        gs.SpawnTile(true);

        // Destroy extra tiles after player leaves collider
        Destroy(gameObject, 10f);
    }
    
    void SpawnDiamonds()
    {
        int diamondsToSpawn = 5;

        for (int i = 0; i < diamondsToSpawn; i++)
        {
            // Spawing diamonds at a random point on ground
            GameObject temp = Instantiate(pDiamondPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    // Getting random point in tile to spawn diamonds
    Vector3 GetRandomPointInCollider (Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)

        );

        // Checks if random point generated is on the collider
        if (point != collider.ClosestPoint(point)){
            // If it is not on the collider it will generate a new point 
            // to position the new diamond
            point = GetRandomPointInCollider(collider);
        }

        // Ensures diamonds are same height
        point.y = 1f;
        return point;
    }
    
    // Spawning obstacles
    
    // Initializing objects for obstacles
    public GameObject obstaclePrefab; 
    public GameObject springObstaclePrefab;
    public GameObject bumperObstaclePrefab;

    // Methods for 3 obstacles to spawn

    public void SpawnObstacle()
    {
        // Gets  obstacle location on ground prefab
        int obstacleSpawnIndex = Random.Range(4, 7);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(obstaclePrefab,  spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnSpringObstacle()
    {   
        int obstacleSpawnIndex = Random.Range(7, 10);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(springObstaclePrefab,  spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnBumperObstacle()
    {   
        int obstacleSpawnIndex = Random.Range(14, 17);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        Instantiate(bumperObstaclePrefab,  spawnPoint.position, Quaternion.identity, transform);
        
    }
}
