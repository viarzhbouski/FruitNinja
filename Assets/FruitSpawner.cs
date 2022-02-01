using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    private GameConfig gameConfig;
    
    [SerializeField]
    private Vector2 spawnPoint;
    
    [SerializeField]
    private GameObject fruit;
    
    [SerializeField]
    private Canvas gameField;

    private int yOffset = -5;
    private float currentTimeDelay = 0;
    
    void Start()
    {
        gameConfig = GetComponent<GameConfig>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        if (currentTimeDelay >= 0)
        {
            currentTimeDelay -= Time.deltaTime;
            return;
        }

        if (fruit != null && gameField != null)
        {
            var spawnZone = GetSpawnZone();
            if (spawnZone == null)
            {
                return;
            }

            var position = GetPosition(spawnZone);
            var directionVector = GetFruitMovementVector(spawnZone) * gameConfig.Speed;

            var newFruit = Instantiate(fruit, position, Quaternion.identity,
                gameField.transform);

            var fruitMovement = newFruit.GetComponent<FruitMovement>();
            fruitMovement.SetMovementConfig(directionVector, gameConfig.GravityVector);
            currentTimeDelay = gameConfig.SpawnDelay;
        }
    }

    private SpawnZone? GetSpawnZone()
    {
        if (gameConfig == null)
        {
            return null;
        }

        SpawnZone spawnZone;
        
        var spawnZonesCount = gameConfig.BottomSpawnZones.Count();

        if (spawnZonesCount == 1)
        {
            spawnZone = gameConfig.BottomSpawnZones.First();
        }
        else
        {
            var spawnZoneId = Random.Range(0, spawnZonesCount - 1);
            spawnZone = gameConfig.BottomSpawnZones[spawnZoneId];
        }
        
        return spawnZone;
    }

    private Vector3 GetPosition(SpawnZone spawnZone)
    {
        var positon = Random.Range(spawnZone.From, spawnZone.To);
        var rect = gameField.GetComponent<RectTransform>();
        return new Vector3(positon, yOffset, rect.position.z);
    }

    private Vector3 GetFruitMovementVector(SpawnZone spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180 ;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }
}
