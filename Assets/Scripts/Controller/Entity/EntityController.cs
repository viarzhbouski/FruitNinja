using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] 
    private protected SpriteRenderer shadowSpriteRenderer;
    [SerializeField] 
    private protected SpriteRenderer spriteRenderer;
    [SerializeField] 
    private protected EntityPhysics entityPhysics;
    
    private protected EntityRepositoryController _entityRepositoryController;
    private protected SwipeController _swipeController;
    private protected LifeCountController _lifeCountController;
    private protected GameConfig _gameConfig;
    private protected EntityConfig _entityConfig;
    private protected EntityOnGameFieldChecker _entityOnGameFieldChecker;
    private protected SpriteRenderer _shadowSpriteRenderer;
    private protected bool _entityCanCut;
    private protected delegate void EntityOutOfBorder();
    private protected event EntityOutOfBorder _entityOutOfBorder; 
    
    private float _rotateSpeed;

    public EntityPhysics EntityPhysics
    {
        get { return entityPhysics; }
    }
    
    private protected void SetEntityConfig(Vector3 directionVector,
                                EntityConfig entityConfig,
                                SwipeController swipeController,
                                LifeCountController lifeCountController,
                                EntityRepositoryController entityRepositoryController,
                                EntityOnGameFieldChecker entityOnGameFieldChecker,
                                Sprite sprite = null)
    {
        _entityConfig = entityConfig;
        _swipeController = swipeController;
        _lifeCountController = lifeCountController;
        _entityRepositoryController = entityRepositoryController;
        _entityOnGameFieldChecker = entityOnGameFieldChecker;
        _gameConfig = entityOnGameFieldChecker.GameConfigManager.GameConfig;
        _rotateSpeed = Random.Range(0, _entityConfig.RotateSpeed);
        _shadowSpriteRenderer = Instantiate(shadowSpriteRenderer, transform.position, Quaternion.identity, transform.parent);
        
        _entityRepositoryController.Entities.Add(this);
        _shadowSpriteRenderer.sprite = sprite == null ? _entityConfig.EntitySprite : sprite;
        spriteRenderer.sprite = sprite == null ? _entityConfig.EntitySprite : sprite;
        
        entityPhysics.GravityVector = _gameConfig.GravityVector;
        entityPhysics.DirectionVector = directionVector;
    }

    private protected void EntityDestroy()
    {
        _entityRepositoryController.Entities.Remove(this);
        Destroy(_shadowSpriteRenderer.gameObject);
        Destroy(gameObject);
    }

    private protected void UpdateEntity()
    {
        UpdateShadowPosition();
        
        if (_lifeCountController.GameOver)
        {
            return;
        }
        
        if (!_entityOnGameFieldChecker.EntityOnGameField(transform.position.x, transform.position.y))
        {
            _entityOutOfBorder?.Invoke();
            EntityDestroy();
        }

        EntitySwipeCheckCollision();
        transform.Rotate(0f, 0f, entityPhysics.DirectionVector.x > 0 ? _rotateSpeed : -_rotateSpeed, Space.Self);
    }
    
    private void UpdateShadowPosition()
    {
        var entityPosition = transform.position;
        entityPosition.y = entityPosition.y - 1.5f;
        entityPosition.z = entityPosition.z + 1;
        
        _shadowSpriteRenderer.transform.position = entityPosition;
        _shadowSpriteRenderer.transform.rotation = transform.rotation;
    }
    
    private void EntitySwipeCheckCollision()
    {
        var from = new Vector3(transform.position.x, transform.position.y, 0);
        var to = new Vector3(_swipeController.Swipe.transform.position.x, _swipeController.Swipe.transform.position.y, 0);
        var distance = Vector3.Distance(from, to);

        if (distance <= _gameConfig.MinDistanceForCutFruit && 
            _swipeController.Velocity > _gameConfig.MinVelocityForCutFruit)
        {
            _entityCanCut = true;
        }
    }
}
