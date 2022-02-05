using System;
using System.Linq;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawnController : MonoBehaviour
{
    [SerializeField]
    private Canvas gameField;
    [SerializeField]
    private RectTransform gameFieldRectTransform;
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private SwipeController swipeController;
    [SerializeField]
    private ScoreCountController scoreCountController;
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField] 
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    
    private GameConfig gameConfig;
    private float currentSpawnFruitPackDelay;
    private float currentSpawnFruitDelay;
    
    void Start()
    {
        gameConfig = gameConfigController.GameConfig;
        currentSpawnFruitPackDelay = 0;
        currentSpawnFruitDelay = 0;
    }
    
    void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        SpawnFruitPack();
    }

    private void SpawnFruitPack()
    {
        if (currentSpawnFruitPackDelay >= 0)
        {
            currentSpawnFruitPackDelay -= Time.deltaTime;
            return;
        }
        
        var spawnZone = GetSpawnZone();
        var position = GetPosition(spawnZone);
        
        StartCoroutine(SpawnFruit(spawnZone, position));

        currentSpawnFruitPackDelay = gameConfig.SpawnFruitPackDelay;
    }
    
    IEnumerator SpawnFruit(SpawnZoneConfig spawnZone, Vector3 position)
    {
        for (int i = 0; i < gameConfig.FruitCountInPack; i++)
        {
            yield return new WaitForSeconds(gameConfig.SpawnFruitDelay);
            
            var fruit = GetFruit();
            var directionVector = GetFruitMovementVector(spawnZone) * fruit.FruitSpeed * spawnZone.SpeedMultiplier;
            var spawnedFruit = Instantiate(fruit.FruitPrefab, position, Quaternion.identity, gameField.transform);

            spawnedFruit.GetComponent<FruitController>()
                .SetFruitConfig(directionVector, fruit, swipeController, scoreCountController, lifeCountController,
                    entityOnGameFieldChecker);
        }
    }

    private SpawnZoneConfig GetSpawnZone()
    {
        SpawnZoneConfig spawnZone;
        
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

    private Vector3 GetPosition(SpawnZoneConfig spawnZone)
    {
        var position = Random.Range(spawnZone.From, spawnZone.To);
        
        switch (spawnZone.SpawnZonePosition)
        {
            case SpawnZonePosition.Bottom:
                return new Vector3(position, gameConfig.YMinBorder, gameFieldRectTransform.position.z);
            case SpawnZonePosition.Left:
                return new Vector3(gameConfig.XMinBorder, position, gameFieldRectTransform.position.z);
            case SpawnZonePosition.Right:
                return new Vector3(gameConfig.XMaxBorder, position, gameFieldRectTransform.position.z);
        }
        
        return Vector3.zero;
    }

    private Vector3 GetFruitMovementVector(SpawnZoneConfig spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }

    private FruitConfig GetFruit()
    {
        if (gameConfig.Fruits.Count() == 1)
        {
            return gameConfig.Fruits.First();
        }
        
        var fruitId = Random.Range(0, gameConfig.Fruits.Count() - 1);
        return gameConfig.Fruits[fruitId];
    }
}
