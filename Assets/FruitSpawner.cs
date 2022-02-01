using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private Canvas gameField;
    
    [SerializeField]
    private GameObject tapObject;
    
    private GameConfig gameConfig;
    private RectTransform rectTransform;

    private int xOffset;
    private int yOffset;
    private float currentTimeDelay;
    
    void Start()
    {
        gameConfig = GetComponent<GameConfig>();
        rectTransform = gameField.GetComponent<RectTransform>();
        xOffset = gameConfig.XOffset;
        yOffset = gameConfig.YOffset;
        currentTimeDelay = 0;
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
            
            fruitMovement.SetMovementConfig(directionVector, gameConfig.GravityVector, tapObject);
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
        
        switch (spawnZone.SpawnZonePosition)
        {
            case SpawnZonePosition.Bottom:
                return new Vector3(position, yOffset, rectTransform.position.z);
            case SpawnZonePosition.Left:
                return new Vector3(-xOffset, position, rectTransform.position.z);
            case SpawnZonePosition.Right:
                return new Vector3(xOffset, position, rectTransform.position.z);
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
