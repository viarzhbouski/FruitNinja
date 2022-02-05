using System;
using System.Linq;
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
    private float currentTimeDelay;

    void Start()
    {
        gameConfig = gameConfigController.GameConfig;
        currentTimeDelay = 0;
    }
    
    void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        if (currentTimeDelay >= 0)
        {
            currentTimeDelay -= Time.deltaTime;
            return;
        }
        
        var spawnZone = GetSpawnZone();
        var position = GetPosition(spawnZone);
        var fruit = GetFruit();
        var directionVector = GetFruitMovementVector(spawnZone) * fruit.FruitSpeed * spawnZone.SpeedMultiplier;
        var newFruit = Instantiate(fruit.FruitPrefab, position, Quaternion.identity, gameField.transform);
        
        newFruit.GetComponent<FruitController>()
                .SetFruitConfig(directionVector, fruit, swipeController, scoreCountController, lifeCountController, entityOnGameFieldChecker);
        
        currentTimeDelay = gameConfig.SpawnDelay;
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
