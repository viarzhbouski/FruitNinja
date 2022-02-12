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
    [SerializeField]
    private GameObject shadowPrefab;
    
    private GameObject shadow;
    private GameConfig _gameConfig;
    private FruitConfig _fruitConfig;
    private SwipeController _swipeController;
    private ScoreCountController _scoreCountController;
    private LifeCountController _lifeCountController;
    private ComboController _comboController;
    private EntityOnGameFieldChecker _entityOnGameFieldChecker;
    private bool _fruitCanCut;
    private float _rotateSpeed;

    private void Start()
    {
        shadow = Instantiate(shadowPrefab, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void Update()
    {
        UpdateShadowPosition();
        
        if (_lifeCountController.GameOver)
        {
            return;
        }
        
        if (_fruitCanCut)
        {
            SpawnSprayffect();
            SpawnCutEffect();
            SpawnFruitFragments();
            _scoreCountController.AddScore(_fruitConfig.Score * _comboController.ComboMultiplier, transform.position);
            _comboController.FruitCutEvent.Invoke();
            Destroy(shadow);
            Destroy(gameObject);
        }
        
        if (!_entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            _lifeCountController.DecreaseLife();
            Destroy(shadow);
            Destroy(gameObject);
        }

        FruitSwipeCheckCollision();
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? _rotateSpeed : -_rotateSpeed, Space.Self);
    }
    
    public void SetFruitConfig(Vector3 directionVector,
        FruitConfig fruitConfig, 
        SwipeController swipeController, 
        ScoreCountController scoreCountController, 
        LifeCountController lifeCountController, 
        ComboController comboController, 
        EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        _fruitConfig = fruitConfig;
        _swipeController = swipeController;
        _scoreCountController = scoreCountController;
        _lifeCountController = lifeCountController;
        _comboController = comboController;
        _entityOnGameFieldChecker = entityOnGameFieldChecker;
        _gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        _rotateSpeed = Random.Range(0, _fruitConfig.FruitRotateSpeed);
        spriteRenderer.sprite = _fruitConfig.FruitSprite;
        entityPhysics.GravityVector = _gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }

    private void UpdateShadowPosition()
    {
        var fruitPosition = transform.position;
        fruitPosition.y = fruitPosition.y - 1.5f;
        fruitPosition.z = fruitPosition.z + 1;
        
        shadow.transform.position = fruitPosition;
    }

    private void FruitSwipeCheckCollision()
    {
        var from = new Vector3(transform.position.x, transform.position.y, 0);
        var to = new Vector3(_swipeController.Swipe.transform.position.x, _swipeController.Swipe.transform.position.y, 0);
        var distance = Vector3.Distance(from, to);

        if (distance <= _gameConfig.MinDistanceForCutFruit && 
            _swipeController.Velocity > _gameConfig.MinVelocityForCutFruit)
        {
            _fruitCanCut = true;
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
            var x = Random.Range(-_fruitConfig.FragmentSpeed, _fruitConfig.FragmentSpeed);
            var y = Random.Range(-_fruitConfig.FragmentSpeed, _fruitConfig.FragmentSpeed);
            var vector = new Vector3(x, y, 0);
            var spawnedFragment = Instantiate(fragment, transform.position, Quaternion.identity, transform.parent);
            var spawnedFragmentTransform = spawnedFragment.transform;
            var spawnedFragmentPosition = spawnedFragmentTransform.position;
            
            spawnedFragmentPosition = new Vector3(spawnedFragmentPosition.x, spawnedFragmentPosition.y, spawnedFragmentPosition.z - 2);
            spawnedFragmentTransform.position = spawnedFragmentPosition;
            spawnedFragment.SetFruitFragmentConfig(vector, _entityOnGameFieldChecker, fragmentSprite, _fruitConfig.FragmentRotateSpeed);
            startY += offsetY;
            pivot.y -= 0.5f;
        }
    }

    private void SpawnCutEffect()
    {
        var cutEffect = _gameConfig.CutEffect;
        var main = cutEffect.main;
        var trail = cutEffect.trails;
        
        main.startColor = _fruitConfig.CutEffectColor;
        trail.colorOverLifetime = _fruitConfig.CutEffectColor;
        trail.colorOverTrail = _fruitConfig.CutEffectColor;
        Instantiate(cutEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void SpawnSprayffect()
    {
        var sprayEffect = _gameConfig.SprayEffect;
        var colorOverLifetime = sprayEffect.colorOverLifetime;
        var ourGradient = new Gradient();
        
        ourGradient.SetKeys(
            new [] { new GradientColorKey(_fruitConfig.CutEffectColor, 0.0f)},
            new [] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f)}
        );
        colorOverLifetime.color = ourGradient;
        
        var effect = Instantiate(sprayEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
        var position = effect.transform.position;
        
        position = new Vector3(position.x, position.y, position.z + 2);
        effect.transform.position = position;
    }
}
