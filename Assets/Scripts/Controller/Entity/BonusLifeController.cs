using UnityEngine;

public class BonusLifeController : EntityController
{
    void Update()
    {
        UpdateEntity();
        
        if (_entityCanCut)
        {
            BonusLifeCut();
        }
    }

    private void BonusLifeCut()
    {
        SpawnCutBonusLifeEffect();
        _lifeCountController.EncreaseLife();
        EntityDestroy();
    }

    public void SetBonusLifeConfig(Vector3 directionVector,
        BonusLifeConfig bonusLifeConfig, 
        SwipeController swipeController,
        LifeCountController lifeCountController,
        EntityRepositoryController entityRepositoryController,
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        SetEntityConfig(directionVector, bonusLifeConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
    }
    
    private void SpawnCutBonusLifeEffect()
    {
        var cutBonusLifeEffect = _gameConfig.CutBonusLifeEffect;
        Instantiate(cutBonusLifeEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}