using UnityEngine;

public class BonusFreezeController : EntityController
{
    private void Update()
    {
        UpdateEntity();
        
        if (EntityCanCut)
        {
            BonusFreezeCut();
        }
    }
    
    private void BonusFreezeCut()
    {
        var config = (BonusFreezeConfig)EntityConfig;
        EntityControllersProvider.GameTimeController.FreezeTime(config.FreezeForce, config.FreezeTime);
        SpawnCutBonusFreezeEffect();
        EntityDestroy();
    }

    private void SpawnCutBonusFreezeEffect()
    {
        var cutBonusFreezeEffect = GameConfig.CutBonusFreezeEffect;
        Instantiate(cutBonusFreezeEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}
