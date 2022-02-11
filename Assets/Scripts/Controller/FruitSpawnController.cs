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
    private Transform gameField;
    
    private GameConfig gameConfig;
    private float currentSpawnFruitPackDelay;
    private float currentSpawnFruitDelay;
    private bool canStartGame;
    
    void Start()
    {
        gameConfig = gameConfigController.GameConfig;
        currentSpawnFruitPackDelay = 0;
        currentSpawnFruitDelay = 0;
        StartCoroutine(DelayBeforeStart());
    }
    
    void Update()
    {
        if (lifeCountController.GameOver || !canStartGame)
        {
            return;
        }
        
        SpawnFruitPack();
    }

    IEnumerator DelayBeforeStart()
    {
        yield return new WaitForSeconds(gameConfig.DelayBeforeStart);
        canStartGame = true;
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

        currentSpawnFruitPackDelay = difficultyLogicController.FruitPackDelay;
    }
    
    IEnumerator SpawnFruit(SpawnZoneConfig spawnZone, Vector3 position)
    {
        for (int i = 0; i < difficultyLogicController.FruitCountInPack; i++)
        {
            yield return new WaitForSeconds(difficultyLogicController.FruitDelay);
            
            var fruit = GetFruit();
            var directionVector = GetFruitMovementVector(spawnZone) * fruit.FruitSpeed * spawnZone.SpeedMultiplier;
            var spawnedFruit = Instantiate(fruit.FruitController, position, Quaternion.identity, gameField);
            spawnedFruit.transform.localPosition = new Vector3(spawnedFruit.transform.localPosition.x,
                spawnedFruit.transform.localPosition.y, Vector3.zero.z);
            spawnedFruit.SetFruitConfig(directionVector, fruit, swipeController, scoreCountController, lifeCountController, entityOnGameFieldChecker);
            
            //spawnedFruit.transform.SetSiblingIndex(1);
            //spawnedFruit.GetComponent<FruitController>()
               // .SetFruitConfig(directionVector, fruit, swipeController, scoreCountController, lifeCountController,
                 //   entityOnGameFieldChecker);
        }
    }
    
    private SpawnZoneConfig GetSpawnZone()
    {
        if (gameConfig.SpawnZones.Count == 1)
        {
            return gameConfig.SpawnZones.First();
        }
        
        var sumChance = gameConfig.SpawnZones.Sum(e => e.Chance);
        var randomNum = Random.Range(0, sumChance);

        SpawnZoneConfig spawnZone = null;
        
        foreach (var item in gameConfig.SpawnZones)
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
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(position, gameConfig.YMinBorder));
            case SpawnZonePosition.Left:
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(gameConfig.XMinBorder,position));
            case SpawnZonePosition.Right:
                return swipeController.Camera.ViewportToWorldPoint(new Vector2(gameConfig.XMaxBorder,position));
        }

        return Vector3.zero;;
    }

    private Vector3 GetFruitMovementVector(SpawnZoneConfig spawnZone)
    {
        var angleRad = Random.Range(spawnZone.MinAngle, spawnZone.MaxAngle) * Mathf.PI / 180;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
    }

    private FruitConfig GetFruit()
    {
        if (gameConfig.Fruits.Count == 1)
        {
            return gameConfig.Fruits.First();
        }
        
        var fruitId = Random.Range(0, gameConfig.Fruits.Count);
        
        return gameConfig.Fruits[fruitId];
    }
}
