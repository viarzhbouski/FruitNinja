using UnityEngine;

public class BombController : EntityController
{
    void Update()
    {
        UpdateEntity();
        
        if (EntityCanCut)
        {
            BombCut();
        }
    }

    private void BombCut()
    {
        EntityControllersProvider.LifeCountController.DecreaseLife();
        PushEntitites();
        SpawnExplodeEffect();
        EntityDestroy();
    }

    private void PushEntitites()
    {
        var explodeForce = ((BombConfig)EntityConfig).ExplodeForce;

        foreach (var entity in EntityControllersProvider.EntityRepositoryController.Entities)
        {
            var newVector = entity.transform.position - transform.position;
            var speed = explodeForce - newVector.magnitude;
            
            if (speed < 0)
                speed = 0;
            
            entity.PushEntity(newVector * speed);
        }
    }

    private void SpawnExplodeEffect()
    {
        Instantiate(GameConfig.ExplodeEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
