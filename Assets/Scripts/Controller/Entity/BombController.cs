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
        _lifeCountController.DecreaseLife();
        SpawnExplodeEffect();
        EntityDestroy();
    }
    
    public void SetBombConfig(Vector3 directionVector,
        BombConfig bombConfig, 
        SwipeController swipeController,
        LifeCountController lifeCountController,
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        SetEntityConfig(directionVector, bombConfig, swipeController, lifeCountController, entityOnGameFieldChecker);
    }

    private void SpawnExplodeEffect()
    {
        Instantiate(_gameConfig.ExplodeEffect, transform.position, Quaternion.identity, transform.parent);
    }
}
