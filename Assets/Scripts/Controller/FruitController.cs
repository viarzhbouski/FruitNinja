using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private EntityPhysics entityPhysics;
    //[SerializeField] 
    //private GameObject[] fragments;
    //[SerializeField]
    //private SpriteRenderer fragment;
    
    
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

        spriteRenderer.sprite = fruit.FruitSprite;
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
            SpawnSprayffect();
            SpawnCutEffect();
            scoreCountController.AddScore();
            //A();
            //SpawnFruitFragments();
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

    private void A()
    {
        Texture2D texture = spriteRenderer.sprite.texture;
        var rect1 = new Rect(0, texture.height / 2, texture.width, texture.height / 2);
        var rect2 = new Rect(0, texture.height / 2, texture.width, texture.height / 2);
        var sprite1 = Sprite.Create(texture, rect1, Vector2.zero);
        var sprite2 = Sprite.Create(texture, rect1, Vector2.zero);
        //var sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        var a1 = Instantiate(this, transform.position, Quaternion.identity, transform.parent);
        var a2 = Instantiate(this, transform.position, Quaternion.identity, transform.parent);
        
        var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
        var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
        var vector = new Vector3(x, y, 0);
        a1.SetFruitConfig(vector, fruit, swipeController, scoreCountController, lifeCountController, entityOnGameFieldChecker);
        a1.GetComponent<SpriteRenderer>().sprite = sprite1;
        x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
        y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
        vector = new Vector3(x, y, 0);
        a2.SetFruitConfig(vector, fruit, swipeController, scoreCountController, lifeCountController, entityOnGameFieldChecker);
        a2.GetComponent<SpriteRenderer>().sprite = sprite2;
        //a1.sprite = sprite1;
        //a2.sprite = sprite2;
    }

    private void SpawnFruitFragments()
    {
        /*for (int i = 0; i < fragments.Length; i++)
        {
            var spawnedFragment = Instantiate(fragments[i], transform.position, Quaternion.identity, transform.parent);
            spawnedFragment.transform.SetSiblingIndex(1);
            
            var x = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var y = Random.Range(-fruit.FragmentSpeed, fruit.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            spawnedFragment.GetComponent<FruitFragmentController>()
                           .SetFruitFragmentConfig(entityOnGameFieldChecker, vector, fruit.FragmentRotateSpeed);
        }*/
    }

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
