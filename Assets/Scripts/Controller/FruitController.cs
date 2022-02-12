using UnityEngine;
using Random = UnityEngine.Random;

public class FruitController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private EntityPhysics entityPhysics;
    [SerializeField]
    private FruitFragmentController fragment;
    
    private FruitConfig fruit;
    private SwipeController swipeController;
    private ScoreCountController scoreCountController;
    private LifeCountController lifeCountController;
    private EntityOnGameFieldChecker entityOnGameFieldChecker;
    private GameConfig gameConfig;
    
    private bool fruitCanCut;
    private float rotateSpeed;

    public void SetFruitConfig(Vector3 directionVector,
        FruitConfig fruit, 
        SwipeController swipeController, 
        ScoreCountController scoreCountController, 
        LifeCountController lifeCountController, 
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        this.fruit = fruit;
        this.swipeController = swipeController;
        this.scoreCountController = scoreCountController;
        this.lifeCountController = lifeCountController;
        this.entityOnGameFieldChecker = entityOnGameFieldChecker;
        
        spriteRenderer.sprite = fruit.FruitSprite;
        gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        entityPhysics.GravityVector = gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
        rotateSpeed = Random.Range(0, fruit.FruitRotateSpeed);
    }
    
    private void Update()
    {
        if (lifeCountController.GameOver)
        {
            return;
        }
        
        if (fruitCanCut)
        {
            SpawnSprayffect();
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
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? rotateSpeed : -rotateSpeed, Space.Self);
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
        var offsetY = spriteRenderer.sprite.texture.height / 2;
        var startY = 0;
        
        for (var i = 0; i < 2; i++)
        {
            var texture = spriteRenderer.sprite.texture;
            var rect = new Rect(0, startY, texture.width, offsetY);
            var fragmentSprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
            var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            var spawnedFragment = Instantiate(fragment, transform.position, Quaternion.identity, transform.parent);
            
            spawnedFragment.SetFruitFragmentConfig(vector, entityOnGameFieldChecker, fragmentSprite, fruit.FragmentRotateSpeed);
            startY += offsetY;
        }
    }

    /*private void SpawnFruitFragments()
    {
        for (int i = 0; i < fragments.Length; i++)
        {
            var spawnedFragment = Instantiate(fragments[i], transform.position, Quaternion.identity, transform.parent);
            spawnedFragment.transform.SetSiblingIndex(1);
            
            var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            spawnedFragment.GetComponent<FruitFragmentController>()
                           .SetFruitFragmentConfig(entityOnGameFieldChecker, vector, fruit.FragmentRotateSpeed);
        }
    }*/

    private void SpawnCutEffect()
    {
        var cutEffect = gameConfig.CutEffect;
        var main = cutEffect.main;
        var trail = cutEffect.trails;
        
        main.startColor = fruit.CutEffectColor;
        trail.colorOverLifetime = fruit.CutEffectColor;
        trail.colorOverTrail = fruit.CutEffectColor;
        
        var effect = Instantiate(cutEffect.gameObject, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity, transform.parent);
        effect.transform.SetSiblingIndex(1);
    }
    
    private void SpawnSprayffect()
    {
        var sprayEffect = gameConfig.SprayEffect;
        var colorOverLifetime = sprayEffect.colorOverLifetime;
        var ourGradient = new Gradient();
        
        ourGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(fruit.CutEffectColor, 0.0f)},
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f)}
        );
        colorOverLifetime.color = ourGradient;
        
        var effect = Instantiate(sprayEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
        effect.transform.SetSiblingIndex(1);
    }
}
