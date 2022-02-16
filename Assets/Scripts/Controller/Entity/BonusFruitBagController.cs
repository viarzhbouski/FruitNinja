using UnityEngine;

public class BonusFruitBagController : EntityController
{
    private void Update()
    {
        UpdateEntity();
        
        if (EntityCanCut)
        {
            BonusFruitBagCut();
        }
    }

    private void BonusFruitBagCut()
    {
        for (var i = 0; i < GameConfig.BonusFruitBag.FruitCountInBag; i++)
        {
            var fruitNum = Random.Range(0, GameConfig.Fruits.Count - 1);
            var fruitConfig = GameConfig.Fruits[fruitNum];
            var vector = new Vector3(Random.Range(-0.5f, 0.5f),  1f, 0);
            var directionVector = vector * GameConfig.BonusFruitBag.FruitSpeed;
            var entityController = EntityControllersProvider.EntitySpawnController.SpawnEntity(directionVector, transform.position, fruitConfig);
            
            entityController.DisableCutEntityByDelay(GameConfig.BonusFruitBag.FruitSwipeDelay);
        }
        
        EntityDestroy();
    }
}