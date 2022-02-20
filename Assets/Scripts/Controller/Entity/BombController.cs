using UnityEngine;

public class BombController : EntityController
{
    private const float DefaultSpeed = 1f;
    
    private void Update()
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
        var explodeRadius = ((BombConfig)EntityConfig).ExplodeRadius;
        
        foreach (var entity in EntityControllersProvider.EntityRepositoryController.Entities)
        {
            if (entity.Equals(this))
            {
                continue;
            }
            
            var newVector = entity.transform.position - transform.position;
            if (newVector.magnitude > explodeRadius)
            {
                continue;
            }
            
            var speed = explodeForce - newVector.magnitude;
            
            if (speed < 0)
                speed = DefaultSpeed;
            
            entity.PushEntity(newVector * speed);
        }
    }

    private void SpawnExplodeEffect()
    {
        Instantiate(GameConfig.ExplodeEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
