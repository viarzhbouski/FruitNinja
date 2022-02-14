using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntitySpawnController : MonoBehaviour
{
    [SerializeField]
    private EntityRepositoryController entityRepositoryController;
    [SerializeField]
    private GameConfigController gameConfigController;
    [SerializeField]
    private SwipeController swipeController;
    [SerializeField]
    private ScoreCountController scoreCountController;
    [SerializeField]
    private LifeCountController lifeCountController;
    [SerializeField]
    private DifficultyLogicController difficultyLogicController;
    [SerializeField] 
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    [SerializeField] 
    private ComboController comboController;
    [SerializeField] 
    private Transform gameField;

    private List<EntityConfig> _entityConfigs;
    private GameConfig _gameConfig;
    private float _currentSpawnFruitPackDelay;
    private bool _canStartGame;
    
    void Start()
    {
        _gameConfig = gameConfigController.GameConfig;
        _currentSpawnFruitPackDelay = 0;
        StartCoroutine(DelayBeforeStart());
        
        _entityConfigs = new List<EntityConfig>(_gameConfig.Fruits);
        _entityConfigs.Add(_gameConfig.Bomb);
        _entityConfigs.Add(_gameConfig.BonusLife);
    }
    
    void Update()
    {
        if (lifeCountController.GameOver || !_canStartGame)
        {
            return;
        }
        
        SpawnFruitPack();
    }

    IEnumerator DelayBeforeStart()
    {
        yield return new WaitForSeconds(_gameConfig.DelayBeforeStart);
        _canStartGame = true;
    }

    private void SpawnFruitPack()
    {
        if (_currentSpawnFruitPackDelay >= 0)
        {
            _currentSpawnFruitPackDelay -= Time.deltaTime;
            return;
        }
        
        var spawnZone = GetSpawnZone();
        var position = GetPosition(spawnZone);
        StartCoroutine(SpawnFruit(spawnZone, position));
        
        _currentSpawnFruitPackDelay = difficultyLogicController.FruitPackDelay;
    }
    
    IEnumerator SpawnFruit(SpawnZoneConfig spawnZone, Vector3 position)
    {
        for (int i = 0; i < difficultyLogicController.FruitCountInPack; i++)
        {
            yield return new WaitForSeconds(difficultyLogicController.FruitDelay);
            var entityConfig = GetEntityConfig();
            SpawnEntity(spawnZone, position, entityConfig);
        }
    }

    public void SpawnEntity(SpawnZoneConfig spawnZone, Vector3 position, EntityConfig entityConfig, Vector3? vector = null, Sprite sprite = null)
    {
        var directionVector = vector ?? GetMovementVector(spawnZone) * entityConfig.Speed * spawnZone.SpeedMultiplier;
        var spawnedEntity = Instantiate(entityConfig.EntityController, position, Quaternion.identity, gameField);
        var spawnedEntityTransform = spawnedEntity.transform;
        var spawnedEntityPosition = spawnedEntityTransform.localPosition;
            
        spawnedEntityPosition = new Vector3(spawnedEntityPosition.x, spawnedEntityPosition.y, Vector3.zero.z);
        spawnedEntityTransform.localPosition = spawnedEntityPosition;
        
        if (entityConfig is FruitConfig)
        {
            ((FruitController)spawnedEntity).SetFruitConfig(directionVector, (FruitConfig)entityConfig, swipeController, scoreCountController, lifeCountController, comboController, entityRepositoryController, this, entityOnGameFieldChecker);
        }
        else if (entityConfig is BombConfig)
        {
            ((BombController)spawnedEntity).SetBombConfig(directionVector, (BombConfig)entityConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
        }
        else if (entityConfig is BonusLifeConfig)
        {
            ((BonusLifeController)spawnedEntity).SetBonusLifeConfig(directionVector, (BonusLifeConfig)entityConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
        }
        else if (entityConfig is FruitFragmentConfig)
        {
            ((FruitFragmentController)spawnedEntity).SetFruitFragmentConfig(directionVector, (FruitFragmentConfig)entityConfig, swipeController, lifeCountController, entityRepositoryController,  entityOnGameFieldChecker, sprite!);
        }
    }

    private EntityConfig GetEntityConfig()
    {
        var sumChance = _entityConfigs.Sum(e => e.Chance);
        var randomNum = Random.Range(0, sumChance);

        EntityConfig entityConfig = null;
        
        foreach (var item in _entityConfigs)
        {
            if (randomNum < item.Chance)
            {
                entityConfig = item;
                break;
            }
            
            randomNum -= item.Chance;
        }

        return entityConfig;
    }
    
    private SpawnZoneConfig GetSpawnZone()
    {
        if (_gameConfig.SpawnZones.Count == 1)
        {
            return _gameConfig.SpawnZones.First();
        }
        
        var sumChance = _gameConfig.SpawnZones.Sum(e => e.Chance);
        var randomNum = Random.Range(0, sumChance);

        SpawnZoneConfig spawnZone = null;
        
        foreach (var item in _gameConfig.SpawnZones)
        {
            if (randomNum < item.Chance)
            {
                spawnZone = item;
                break;
            }
            
            randomNum -= item.Chance;
        }
        
        return spawnZone;
    }

    private Vector3 GetPosition(SpawnZoneConfig spawnZone)
    {
        var position = Random.Range(spawnZone.From, spawnZone.To);
        
        switch (spawnZone.SpawnZonePosition)
        {
            case SpawnZonePosition.Bottom:
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(position, _gameConfig.YMinBorder));
            case SpawnZonePosition.Left:
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(_gameConfig.XMinBorder,position));
            case SpawnZonePosition.Right:
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(_gameConfig.XMaxBorder,position));
        }

        return Vector3.zero;
    }

    private Vector3 GetMovementVector(SpawnZoneConfig spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }
}
