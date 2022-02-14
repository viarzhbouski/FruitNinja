using UnityEngine;
using Random = UnityEngine.Random;

public class FruitController : EntityController
{
    private EntitySpawnController _entitySpawnController;
    private ScoreCountController _scoreCountController;
    private ComboController _comboController;

    private void Update()
    {
        UpdateEntity();
        
        if (_entityCanCut)
        {
            FruitCut();
        }
    }

    private void FruitCut()
    {
        SpawnSprayEffect();
        SpawnCutEffect();
        SpawnFruitFragments();
        _scoreCountController.AddScore(((FruitConfig)_entityConfig).Score * _comboController.ComboMultiplier, transform.position);
        _comboController.FruitCutEvent.Invoke();
        EntityDestroy();
    }

    public void SetFruitConfig(Vector3 directionVector,
                               FruitConfig fruitConfig, 
                               SwipeController swipeController, 
                               ScoreCountController scoreCountController, 
                               LifeCountController lifeCountController, 
                               ComboController comboController,
                               EntityRepositoryController entityRepositoryController,
                               EntitySpawnController entitySpawnController,
                               EntityOnGameFieldChecker entityOnGameFieldChecker)
    {
        SetEntityConfig(directionVector, fruitConfig, swipeController, lifeCountController, entityRepositoryController, entityOnGameFieldChecker);
        _scoreCountController = scoreCountController;
        _comboController = comboController;
        _entitySpawnController = entitySpawnController;
        _entityOutOfBorder += _lifeCountController.DecreaseLife;
    }

    public void PushFruit(Vector3 vector)
    {
        entityPhysics.DirectionVector = vector;
    }

    private void SpawnFruitFragments()
    {
        var offsetY = spriteRenderer.sprite.texture.height / 2;
        var startY = 0;
        var pivot = new Vector2(0.5f, 0.75f);
        var fragmentConfig = _gameConfig.FruitFragment;

        for (var i = 0; i < 2; i++)
        {
            var texture = spriteRenderer.sprite.texture;
            var rect = new Rect(0, startY, texture.width, offsetY);
            var fragmentSprite = Sprite.Create(texture, rect, pivot);
            
            var x = Random.Range(-fragmentConfig.Speed, fragmentConfig.Speed);
            var y = Random.Range(-fragmentConfig.Speed, fragmentConfig.Speed);
            
            var vector = new Vector3(x, y, 0);
            _entitySpawnController.SpawnEntity(null, transform.position, fragmentConfig, vector, fragmentSprite);
            
            startY += offsetY;
            pivot.y -= 0.5f;
        }
    }

    private void SpawnCutEffect()
    {
        var cutEffect = _gameConfig.CutEffect;
        var main = cutEffect.main;
        var trail = cutEffect.trails;
        
        main.startColor = ((FruitConfig)_entityConfig).FruitColor;
        trail.colorOverLifetime = ((FruitConfig)_entityConfig).FruitColor;
        trail.colorOverTrail = ((FruitConfig)_entityConfig).FruitColor;
        Instantiate(cutEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
    }
    
    private void SpawnSprayEffect()
    {
        var sprayEffect = _gameConfig.SprayEffect;
        var colorOverLifetime = sprayEffect.colorOverLifetime;
        var ourGradient = new Gradient();
        
        ourGradient.SetKeys(
            new [] { new GradientColorKey(((FruitConfig)_entityConfig).FruitColor, 0.0f)},
            new [] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f)}
        );
        colorOverLifetime.color = ourGradient;
        
        var effect = Instantiate(sprayEffect.gameObject, transform.position, Quaternion.identity, transform.parent);
        var position = effect.transform.position;
        
        position = new Vector3(position.x, position.y, position.z + 2);
        effect.transform.position = position;
    }
}
