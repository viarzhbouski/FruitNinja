using UnityEngine;

public class BonusFreezeController : EntityController
{
    private GameTimeController _gameTimeController;
    
    void Update()
    {
        UpdateEntity();
        
        if (_entityCanCut)
        {
            BonusFreezeCut();
        }
    }
    
    private void BonusFreezeCut()
    {
        SpawnCutBonusFreezeEffect();
        _gameTimeController.FreezeTime(((BonusFreezeConfig) _entityConfig).FreezeForce, ((BonusFreezeConfig) _entityConfig).FreezeTime);
        EntityDestroy();
    }

    public void SetBonusFreezeConfig(Vector3 directionVector,
        BonusFreezeConfig bonusFreezeConfig, 
        SwipeController swipeController,
        LifeCountController lifeCountController,
        EntityRepositoryController entityRepositoryController,
        GameTimeController gameTimeController,
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        _gameTimeController = gameTimeController;
        SetEntityConfig(directionVector, bonusFreezeConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
    }
    
    private void SpawnCutBonusFreezeEffect()
    {
        var cutBonusFreezeEffect = _gameConfig.CutBonusFreezeEffect;
        Instantiate(cutBonusFreezeEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}
