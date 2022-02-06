using UnityEngine;
using Random = UnityEngine.Random;

public class FruitController : MonoBehaviour
{
    [SerializeField] 
    private EntityPhysics entityPhysics;
    [SerializeField] 
    private GameObject[] fragments;
    
    private FruitConfig fruit;
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private GameConfig gameConfig;
    private SwipeController swipeController;
    private ScoreCountController scoreCountController;
    private LifeCountController lifeCountController;
    private bool fruitCanCut;
    
    public void SetFruitConfig(Vector3 directionVector, 
        FruitConfig fruit, 
        SwipeController swipeController, 
        ScoreCountController scoreCountController, 
        LifeCountController lifeCountController, 
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        this.fruit = fruit;
        this.swipeController = swipeController;
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        this.scoreCountController = scoreCountController;
        this.lifeCountController = lifeCountController;
        
        gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        entityPhysics.GravityVector = gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }
    
    private void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        if (fruitCanCut)
        {
            SpawnCutEffect();
            scoreCountController.AddScore();
            SpawnFruitFragments();
            Destroy(gameObject);
        }
        if (!entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            lifeCountController.DecreaseLife();
            Destroy(gameObject);
        }

        FruitSwipeCheckCollision();
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? fruit.FruitRotateSpeed : -fruit.FruitRotateSpeed, Space.Self);
    }

    private void FruitSwipeCheckCollision()
    {
        var from = new Vector3(transform.position.x, transform.position.y, 0);
        var to = new Vector3(swipeController.Swipe.transform.position.x, swipeController.Swipe.transform.position.y, 0);
        var distance = Vector3.Distance(from, to);

        if (distance <= gameConfig.MinDistanceForCutFruit && 
            swipeController.Velocity > gameConfig.MinVelocityForCutFruit)
        {
            fruitCanCut = true;
        }
    }

    private void SpawnFruitFragments()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            var spawnedFragment =  Instantiate(fragments[i], transform.position, Quaternion.identity, transform.parent);
            var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            spawnedFragment.GetComponent<FruitFragmentController>()
                           .SetFruitFragmentConfig(entityOnGameFieldChecker, vector, fruit.FragmentRotateSpeed);
        }
    }

    private void SpawnCutEffect()
    {
        var cutEffect = gameConfig.CutEffect;
        var main = cutEffect.main;
        var trail = cutEffect.trails;
        
        main.startColor = fruit.CutEffectColor;
        trail.colorOverLifetime = fruit.CutEffectColor;
        trail.colorOverTrail = fruit.CutEffectColor;
        
        Instantiate(cutEffect.gameObject, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity, transform.parent);
    }
}
