using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    private GameConfig gameConfig;
    
    [SerializeField]
    private Vector2 spawnPoint;

    [SerializeField]
    private Canvas gameField;

    private int xOffset = 10;
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

        if (gameField != null)
        {
            var spawnZone = GetSpawnZone();
            if (spawnZone == null)
            {
                return;
            }

            var position = GetPosition(spawnZone);
            var directionVector = GetFruitMovementVector(spawnZone) * gameConfig.Speed;
            var fruit = GetFruit();
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
       
        
        var spawnZoneType = Random.Range(0, Enum.GetValues(typeof(SpawnZonePosition)).Length);
        Debug.Log($"Type = {spawnZoneType} Count = {Enum.GetValues(typeof(SpawnZonePosition)).Length}");
        
        var spawnZones = gameConfig.SpawnZones
                                                .Where(e => e.SpawnZonePosition == (SpawnZonePosition)spawnZoneType)
                                                .ToList();

        if (spawnZones.Count() == 1)
        {
            spawnZone = spawnZones.First();
        }
        else
        {
            var spawnZoneId = Random.Range(0, spawnZones.Count - 1);
            spawnZone = gameConfig.SpawnZones[spawnZoneId];
        }
        
        return spawnZone;
    }

    private Vector3 GetPosition(SpawnZone spawnZone)
    {
        var position = Random.Range(spawnZone.From, spawnZone.To);
        var rect = gameField.GetComponent<RectTransform>();

        switch (spawnZone.SpawnZonePosition)
        {
            case SpawnZonePosition.Bottom:
                return new Vector3(position, yOffset, rect.position.z);
            case SpawnZonePosition.Left:
                return new Vector3(-xOffset, position, rect.position.z);
            case SpawnZonePosition.Right:
                return new Vector3(xOffset, position, rect.position.z);
        }
        
        return Vector3.zero;
    }

    private Vector3 GetFruitMovementVector(SpawnZone spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180 ;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }

    private GameObject GetFruit()
    {
        if (gameConfig.Fruits.Count() == 1)
        {
            return gameConfig.Fruits.First();
        }
        
        var fruitId = Random.Range(0, gameConfig.Fruits.Count() - 1);
        return gameConfig.Fruits[fruitId];
    }
}
