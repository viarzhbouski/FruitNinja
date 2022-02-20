using System.Linq;
using System.Collections;
using System.Collections.Generic;
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
    private EntityOnGameFieldCheckerController entityOnGameFieldCheckerController;
    [SerializeField] 
    private ComboController comboController;
    [SerializeField] 
    private GameTimeController gameTimeController;
    [SerializeField] 
    private Transform gameField;

    private EntityControllersProvider _entityControllersProvider;
    private Dictionary<EntityType, float> _entityChances;
    private GameConfig _gameConfig;
    private float _currentSpawnFruitPackDelay;
    private bool _canStartGame;

    private void Start()
    {
        _gameConfig = gameConfigController.GameConfig;
        _currentSpawnFruitPackDelay = 0;
        _entityControllersProvider = new EntityControllersProvider
        {
            SwipeController = swipeController,
            LifeCountController = lifeCountController,
            ComboController = comboController,
            ScoreCountController = scoreCountController,
            GameTimeController = gameTimeController,
            EntitySpawnController = this,
            EntityRepositoryController = entityRepositoryController,
            EntityOnGameFieldCheckerController = entityOnGameFieldCheckerController
        };
        StartCoroutine(DelayBeforeStart());
        FillEntityChancesDict();
    }
    
    private void Update()
    {
        if (lifeCountController.GameOver || !_canStartGame)
        {
            return;
        }
        
        SpawnEntityPack();
    }

    IEnumerator DelayBeforeStart()
    {
        yield return new WaitForSeconds(_gameConfig.DelayBeforeStart);
        _canStartGame = true;
    }

    private void SpawnEntityPack()
    {
        if (_currentSpawnFruitPackDelay >= 0)
        {
            _currentSpawnFruitPackDelay -= Time.deltaTime;
            return;
        }
        
        var spawnZone = GetSpawnZone();
        var position = GetPosition(spawnZone);
        var bonusBlocksAtPack = (int)(difficultyLogicController.FruitCountInPack * _gameConfig.RatioOfBonusBlocksToFruits);
        var entityConfigPack = GetEntityConfigPack(bonusBlocksAtPack);
        
        StartCoroutine(SpawnEntity(entityConfigPack, spawnZone, position));
        
        _currentSpawnFruitPackDelay = difficultyLogicController.FruitPackDelay;
    }
    
    IEnumerator SpawnEntity(List<EntityConfig> entityConfigsPack, SpawnZoneConfig spawnZone, Vector3 position)
    {
        for (int i = 0; i < entityConfigsPack.Count; i++)
        {
            yield return new WaitForSeconds(difficultyLogicController.FruitDelay);
            
            var directionVector = GetMovementVector(spawnZone) * entityConfigsPack[i].Speed * spawnZone.SpeedMultiplier;
            SpawnEntity(directionVector, position, entityConfigsPack[i]);
        }
    }

    public EntityController SpawnEntity(Vector3 directionVector, Vector3 position, EntityConfig entityConfig, Sprite sprite = null)
    {
        var spawnedEntity = Instantiate(entityConfig.EntityController, position, Quaternion.identity, gameField);
        var spawnedEntityTransform = spawnedEntity.transform;
        var spawnedEntityPosition = spawnedEntityTransform.localPosition;
            
        spawnedEntityPosition = new Vector3(spawnedEntityPosition.x, spawnedEntityPosition.y, Vector3.zero.z);
        spawnedEntityTransform.localPosition = spawnedEntityPosition;

        spawnedEntity.SetEntityConfig(directionVector, entityConfig, _entityControllersProvider, sprite);

        return spawnedEntity;
    }

    private void FillEntityChancesDict()
    {
        _entityChances = new Dictionary<EntityType, float>
        {
            {EntityType.Fruit, _gameConfig.FruitChance},
            {EntityType.Bomb, _gameConfig.BombChance},
            {EntityType.BonusLife, _gameConfig.BonusLifeChance},
            {EntityType.BonusFreeze, _gameConfig.BonusFreezeChance},
            {EntityType.BonusFruitBag, _gameConfig.BonusFruitBagChance}
        };
    }

    private List<EntityConfig> GetEntityConfigPack(int bonusBlocksAtPack)
    {
        FruitConfig GetRandomFruit()
        {
            var fruitRandom = Random.Range(0, _gameConfig.Fruits.Count - 1);
            return _gameConfig.Fruits[fruitRandom];
        }

        var pack = new List<EntityConfig>();
        
        for (var i = 0; i < difficultyLogicController.FruitCountInPack; i++)
        {
            var sumChance = _entityChances.Values.Sum();
            var randomNum = Random.Range(0, sumChance);
            var entityType = EntityType.Fruit;

            foreach (var item in _entityChances)
            {
                if (randomNum < item.Value)
                {
                    if (item.Key == EntityType.BonusLife &&
                        lifeCountController.CurrentLifeCount == _gameConfig.MaxLifeCount)
                    {
                        continue;
                    }
                    
                    entityType = item.Key;
                    break;
                }

                randomNum -= item.Value;
            }

            if (pack.Count(e => e.EntityType != EntityType.Fruit) == bonusBlocksAtPack)
            {
                pack.Add(GetRandomFruit());
                continue;
            }

            switch (entityType)
            {
                case EntityType.Fruit:
                    pack.Add(GetRandomFruit());
                    break;
                case EntityType.Bomb:
                    pack.Add(_gameConfig.Bomb);
                    break;
                case EntityType.BonusLife:
                    pack.Add(_gameConfig.BonusLife);
                    break;
                case EntityType.BonusFreeze:
                    pack.Add(_gameConfig.BonusFreeze);
                    break;
                case EntityType.BonusFruitBag:
                    pack.Add(_gameConfig.BonusFruitBag);
                    break;
            }
        }

        return pack;
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
