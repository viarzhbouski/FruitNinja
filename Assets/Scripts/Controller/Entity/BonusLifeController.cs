using UnityEngine;

public class BonusLifeController : EntityController
{
    void Update()
    {
        UpdateEntity();
        
        if (EntityCanCut)
        {
            BonusLifeCut();
        }
    }

    private void BonusLifeCut()
    {
        EntityControllersProvider.LifeCountController.EncreaseLife();
        SpawnCutBonusLifeEffect();
        EntityDestroy();
    }

    private void SpawnCutBonusLifeEffect()
    {
        var cutBonusLifeEffect = GameConfig.CutBonusLifeEffect;
        Instantiate(cutBonusLifeEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
}
