using System.Linq;
using UnityEngine;

public class BombController : EntityController
{
    void Update()
    {
        UpdateEntity();
        
        if (_entityCanCut)
        {
            BombCut();
        }
    }

    private void BombCut()
    {
        PushFruits();
        _lifeCountController.DecreaseLife();
        SpawnExplodeEffect();
        EntityDestroy();
    }

    private void PushFruits()
    {
        var fruits = _entityRepositoryController.Entities.Where(e => e is FruitController).ToList();
        
        for (var i = 0; i < fruits.Count; i++)
        {
            var fruit = (FruitController)fruits[i];
            var newVector = fruit.transform.position - transform.position;
            var speed = ((BombConfig)_entityConfig).ExplodeForce - newVector.magnitude;
            
            if (speed < 0)
                speed = 0;
            
            fruit.PushFruit(newVector * speed);
        }
    }
    
    public void SetBombConfig(Vector3 directionVector,
        BombConfig bombConfig, 
        SwipeController swipeController,
        LifeCountController lifeCountController,
        EntityRepositoryController entityRepositoryController,
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        SetEntityConfig(directionVector, bombConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
    }

    private void SpawnExplodeEffect()
    {
        Instantiate(_gameConfig.ExplodeEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
