using System.Linq;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawnController : MonoBehaviour
{
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
    
    private GameConfig _gameConfig;
    private float _currentSpawnFruitPackDelay;
    private bool _canStartGame;
    
    void Start()
    {
        _gameConfig = gameConfigController.GameConfig;
        _currentSpawnFruitPackDelay = 0;
        StartCoroutine(DelayBeforeStart());
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
            
            var bomb = GetBomb();
            if (bomb != null)
            {
                SpawnBomb(spawnZone, position, bomb);
                continue;
            }
            
            var fruit = GetFruit();
            var directionVector = GetFruitMovementVector(spawnZone) * fruit.FruitSpeed * spawnZone.SpeedMultiplier;
            var spawnedFruit = Instantiate(fruit.FruitController, position, Quaternion.identity, gameField);
            var spawnedFruitTransform = spawnedFruit.transform;
            var spawnedFruitPosition = spawnedFruitTransform.localPosition;
            
            spawnedFruitPosition = new Vector3(spawnedFruitPosition.x, spawnedFruitPosition.y, Vector3.zero.z);
            spawnedFruitTransform.localPosition = spawnedFruitPosition;
            spawnedFruit.SetFruitConfig(directionVector, fruit, swipeController, scoreCountController, lifeCountController, comboController, entityOnGameFieldChecker);
        }
    }

    private void SpawnBomb(SpawnZoneConfig spawnZone, Vector3 position, BombConfig bomb)
    {
        var directionVector = GetFruitMovementVector(spawnZone) * bomb.BombSpeed * spawnZone.SpeedMultiplier;
        var spawnedBomb = Instantiate(bomb.BombController, position, Quaternion.identity, gameField);
        var spawnedBombTransform = spawnedBomb.transform;
        var spawnedBombPosition = spawnedBombTransform.localPosition;
            
        spawnedBombPosition = new Vector3(spawnedBombPosition.x, spawnedBombPosition.y, Vector3.zero.z);
        spawnedBombTransform.localPosition = spawnedBombPosition;
        spawnedBomb.SetBombConfig(directionVector, bomb, swipeController, lifeCountController, entityOnGameFieldChecker);
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
    
    private BombConfig GetBomb()
    {
        var chance = _gameConfig.Bomb.Chance;
        var randomNum = Random.Range(0f, 1f);
        
        if (randomNum <= chance)
        {
            return _gameConfig.Bomb;
        }
        
        return null;
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

    private Vector3 GetFruitMovementVector(SpawnZoneConfig spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }

    private FruitConfig GetFruit()
    {
        if (_gameConfig.Fruits.Count == 1)
        {
            return _gameConfig.Fruits.First();
        }
        
        var fruitId = Random.Range(0, _gameConfig.Fruits.Count);
        
        return _gameConfig.Fruits[fruitId];
    }
}
