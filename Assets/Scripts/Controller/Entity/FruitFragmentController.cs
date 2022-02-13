using UnityEngine;

public class FruitFragmentController : EntityController
{
    private void Update()
    {
        UpdateEntity();
    }
    
    public void SetFruitFragmentConfig(Vector3 directionVector,
        FruitFragmentConfig fruitConfig, 
        SwipeController swipeController, 
        LifeCountController lifeCountController,
        EntityOnGameFieldChecker entityOnGameFieldChecker,
        Sprite sprite)
    {
        SetEntityConfig(directionVector, fruitConfig, swipeController, lifeCountController, entityOnGameFieldChecker, sprite);
    }
}
