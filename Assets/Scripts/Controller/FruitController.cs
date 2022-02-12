using UnityEditor.Sprites;
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
            scoreCountController.AddScore(fruit.Score, transform.position);
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
        var pivot = new Vector2(0.5f, 0.75f);
        for (var i = 0; i < 2; i++)
        {
            var texture = spriteRenderer.sprite.texture;
            var rect = new Rect(0, startY, texture.width, offsetY);
            var fragmentSprite = Sprite.Create(texture, rect, pivot);
            var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            var spawnedFragment = Instantiate(fragment, transform.position, Quaternion.identity, transform.parent);
            
            spawnedFragment.transform.position = new Vector3(spawnedFragment.transform.position.x,
                spawnedFragment.transform.position.y, spawnedFragment.transform.position.z - 2);
            spawnedFragment.SetFruitFragmentConfig(vector, entityOnGameFieldChecker, fragmentSprite, fruit.FragmentRotateSpeed);
            startY += offsetY;
            pivot.y -= 0.5f;
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
        Instantiate(cutEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void SpawnSprayffect()
    {
        var sprayEffect = gameConfig.SprayEffect;
        var colorOverLifetime = sprayEffect.colorOverLifetime;
        var ourGradient = new Gradient();
        
        ourGradient.SetKeys(
            new [] { new GradientColorKey(fruit.CutEffectColor, 0.0f)},
            new [] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f)}
        );
        colorOverLifetime.color = ourGradient;
        var effect = Instantiate(sprayEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
        effect.transform.position = new Vector3(effect.transform.position.x,
            effect.transform.position.y, effect.transform.position.z + 2);
    }
}
